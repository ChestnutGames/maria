
#include "play/Context.h"
#include "play/PlayerMgr.h"
#include "play/Player.h"
#include "play/Scene.h"

#include <PxScene.h>
#include <PxRigidDynamic.h>

#include <new>
#include <cassert>

PlayerMgr::PlayerMgr(Context *ctx)
	: _ctx(ctx)
	, _freelist(nullptr)
	, _slots(nullptr)
	, _size(0)
	, _cap(124)
	, _free(0)
{
	_slots = new Player[_cap];
}


PlayerMgr::~PlayerMgr() {
	delete[] _slots;
}

Player * PlayerMgr::createPlayer() {
	if (_size == _cap) {
		return NULL;
	}
	Player *ptr = nullptr;
	if (_freelist) {
		ptr = _freelist;
		_freelist = _freelist->getNext();
	} else {
		if (_free >= _cap) {
			assert(false);
			// extend;
		}
		ptr = &_slots[_free];
		_free++;
		_size++;
		ptr = new (ptr)Player(_ctx);
	}
	return ptr;
}

void PlayerMgr::releasePlayer(Player **self) {
	(*self)->setNext(_freelist);
	_freelist = (*self);
	(*self) = nullptr;
	_size--;
}

Player * PlayerMgr::getPlayerBySuid(int suid) {
	if (_suplayers.find(suid) != _suplayers.end()) {
		return _suplayers[suid];
	}
	return nullptr;
}

Player * PlayerMgr::getPlayerBySid(int sid) {
	if (_splayers.find(sid) != _splayers.end()) {
		return _splayers[sid];
	}
	return nullptr;
}

void PlayerMgr::addPlayerByUid(Player *p) {
	assert(p != nullptr);
	int suid = p->getUid();
	if (_suplayers.find(suid) != _suplayers.end()) {
		assert(false);
	} else {
		_suplayers[suid] = p;
	}
}

void PlayerMgr::removePlayerByUid(int suid) {
	if (_suplayers.find(suid) != _suplayers.end()) {
		_suplayers.erase(suid);
	}
}

void PlayerMgr::addPlayerBySid(Player *p) {
	assert(p != nullptr);
	int sid = p->getSid();
	if (_splayers.find(sid) != _splayers.end()) {
		assert(false);
	} else {
		_splayers[sid] = p;
	}
}

void PlayerMgr::removePlayerBySid(int sid) {
	if (_splayers.find(sid) != _splayers.end()) {
		_splayers.erase(sid);
	}
}

void PlayerMgr::addPlayerBySession(Player *p) {
	int session = p->getSession();
	_seplayers[session] = p;
}

void PlayerMgr::removePlayerBySession(int session) {
	if (_seplayers.find(session) != _seplayers.end()) {
		_seplayers.erase(session);
	}
}

void PlayerMgr::addPlayer(Player *p) {
	_players.push_back(p);
	p->createRigid();
	_ctx->getScene()->getScene()->addActor(*p->getRigid());
}

void PlayerMgr::removePlayer(Player *p) {
	_players.remove(p);
	_ctx->getScene()->getScene()->removeActor(*p->getRigid());
	releasePlayer(&p);
}

void PlayerMgr::foreach(std::function<void(Player*)> &&cb) {
	for (auto iter = _players.begin(); iter != _players.end(); iter++) {
		cb(*iter);
	}
}