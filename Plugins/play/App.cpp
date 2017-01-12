#include "stdafx.h"
#include "App.h"

#include <iostream>
#include <cstdlib>
#include <cstdarg>

App::App()
	: _exit(false) {
}

App::~App() {	
}

void App::run() {
}

void App::updata(float delta) {
	_ctx.update();
}

int App::join(void *ud) {
	PlayerMgr *playerMgr = _ctx.getPlayerMgr();
	Player *p = playerMgr->createPlayer(ud);
	_ctx.addPlayer(p);
	return p->getId();
}

void App::leave(int id) {
	PlayerMgr *playerMgr = _ctx.getPlayerMgr();
	Player *p = _ctx.getPlayer(id);
	_ctx.removePlayer(id);
	playerMgr->releasePlayer(&p);
}

void App::opcode() {

}