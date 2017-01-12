#include "stdafx.h"
#include "App.h"
#include "Context.h"
#include "Player.h"
#include "PlayerMgr.h"

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

bool App::join(int uid, int sid) {
	PlayerMgr *playerMgr = _ctx.getPlayerMgr();
	if (uid > 0) {
		Player *p = playerMgr->getPlayerBySuid(uid);
		if (p) {
		} else {
			p = playerMgr->createPlayer();
			p->setUid(uid);
			p->setSid(sid);
			playerMgr->addPlayer(p);
			playerMgr->addPlayerByUid(p);
			playerMgr->addPlayerBySid(p);
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
	return true;
}

void App::leave(int suid, int sid) {
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