#include "stdafx.h"
#include "App.h"
#include "Context.h"
#include "Player.h"
#include "PlayerMgr.h"
#include "pack.h"

#include <iostream>
#include <cstdlib>
#include <cstdarg>
#include <cassert>

App::App()
	: _exit(false) {
}

App::~App() {
}

void App::run() {
}

void App::updata(float delta) {
	_ctx.update(delta);
}

void App::start() {
	_ctx.onInit();
}

bool App::join(int uid, int sid, int session) {
	_ctx.log("app join uid:%d, sid:%d, session:%d", uid, sid, session);
	bool success = false;
	try {
		PlayerMgr *playerMgr = _ctx.getPlayerMgr();
		if (uid > 0) {
			Player *p = playerMgr->getPlayerBySuid(uid);
			if (p) {
				p->setSid(sid);
				p->setSession(session);

				if (session > 0) {
					playerMgr->addPlayerBySession(p);
				}

			} else {
				p = playerMgr->createPlayer();
				p->setUid(uid);
				p->setSid(sid);
				p->setSession(session);

				playerMgr->addPlayer(p);
				playerMgr->addPlayerByUid(p);

				if (sid > 0) {
					playerMgr->addPlayerBySid(p);
				}
				if (session > 0) {
					playerMgr->addPlayerBySession(p);
				}
			}
		} else if (sid > 0) {
			Player *p = playerMgr->getPlayerBySid(sid);
			if (p) {
			} else {
				p = playerMgr->createPlayer();
				p->setUid(uid);
				p->setSid(sid);
				playerMgr->addPlayer(p);
				playerMgr->addPlayerByUid(p);
				playerMgr->addPlayerBySid(p);
			}
		}
		success = true;
	} catch (const std::exception& ex) {
		_ctx.log("uid %d found exception", uid);
	} 
	return success;
}

void App::leave(int suid, int sid, int session) {
	PlayerMgr *playerMgr = _ctx.getPlayerMgr();
	Player *p = nullptr;
	if (suid > 0) {
		p = playerMgr->getPlayerBySuid(suid);

		playerMgr->removePlayerByUid(suid);
		if (p->getSid() > 0 && sid > 0) {
			assert(p->getSid() == sid);
		}
		if (sid > 0) {
			playerMgr->removePlayerBySid(sid);
		}
	}
	if (sid > 0) {
		p = playerMgr->getPlayerBySid(sid);
		playerMgr->removePlayerBySid(sid);
	}
}

void App::opcode() {
}

int App::fetch(char *ptr, int len) {
	int idx = 0;
	PlayerMgr *playerMgr = _ctx.getPlayerMgr();
	playerMgr->foreach([&](Player *p) {
		_ctx.log("session: %d", p->getSession());
		WriteInt32(ptr, idx * 20 + 0, p->getSession());
		WriteFnt32(ptr, idx * 20 + 4, p->getX());
		WriteFnt32(ptr, idx * 20 + 8, p->getY());
		WriteFnt32(ptr, idx * 20 + 16, p->getZ());
		idx++;
	});

	return idx * 20;
}