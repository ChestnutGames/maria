using Maria.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Maria {
    public class Application : IDisposable {

        protected global::App _app;
        protected CommandQueue _queue = new CommandQueue();
        protected Semaphore _semaphore = null;
        protected Thread _worker = null;
        protected Context _ctx = null;
        protected EventDispatcher _dispatcher = null;
        protected TimeSync _tiSync = null;
        protected int _lastTi;

        public Application(App app) {
            _app = app;
            _ctx = new Bacon.AppContext(_app, this);
            _dispatcher = _ctx.EventDispatcher;

            _tiSync = new TimeSync();
            _tiSync.LocalTime();
            _lastTi = _tiSync.LocalTime();

            _semaphore = new Semaphore(1, 1);
            _worker = new Thread(new ThreadStart(Worker));
            _worker.IsBackground = true;
            _worker.Start();

            
            //UnityEngine.Application.on
        }

        public void Dispose() {
            _worker.Abort();
        }

        private void Worker() {
            while (true) {
                _semaphore.WaitOne();

                Command command = _queue.Dequeue();
                if (command != null) {
                    _dispatcher.DispatchCmdEvent(command);
                } else {
                }

                int now = _tiSync.LocalTime();
                int delta = now - _lastTi;
                _lastTi = now;
                _ctx.Update(((float)delta) / 100.0f);

                //_tiSync.Sleep(10);

                _semaphore.Release();
            }
        }

        public void Enqueue(Command cmd) {
            _queue.Enqueue(cmd);
        }

        public void OnApplicationFocus(bool hasFocus) {
            //if (isFocus) {
            //    if (_worker.IsAlive) {

            //    }
            //    _worker.A
            //} else {
            //    Debug.Log("离开游戏 激活推送");  //  返回游戏的时候触发     执行顺序 1  
            //}
        }

        public void OnApplicationPause(bool pauseStatus) {
            if (pauseStatus) {
                //_worker.
                //Debug.Log("游戏暂停 一切停止");  // 缩到桌面的时候触发  
                _semaphore.WaitOne();
            } else {
                //Debug.Log("游戏开始  万物生机");  //回到游戏的时候触发 最晚  
                _semaphore.Release();
            }
        }

        public void OnApplicationQuit() {
            _worker.Abort();
        }
    }
}
