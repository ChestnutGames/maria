#include "stdafx.h"
#include "App.h"
#include <iostream>
<<<<<<< HEAD
#include <foundation/Px.h>
#include <common/PxTolerancesScale.h>
#include <extensions/PxExtensionsAPI.h>
=======
#include <cstdlib>
#include <cstdarg>
>>>>>>> 8fce370289c602dbdf76579b5736f5a4a846cb9f

struct data {
	int dummy;
};

App::App()
	: _exit(false) {
}

App::~App() {	
}

void App::run() {
	
}

void App::updata() {
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