#include "stdafx.h"
#include "Context.h"
#include "PlayerMgr.h"
#include <new>

PlayerMgr::PlayerMgr(Context *ctx)
	: _ctx(ctx)
	, _slots(nullptr)
	, _size(0)
	, _free(5)
	, _cap(6) {
	_slots = new Player[_cap];
}


PlayerMgr::~PlayerMgr() {
	delete[] _slots;
}

Player * PlayerMgr::createPlayer(void *ud) {
	if (_size == _cap) {
		return NULL;
	}
	if (_free < 0) {
		_free = _cap - 1;
	}
	Player *ptr = &_slots[_free];
	_free--;
	_size++;
	_idx++;
	ptr = new (ptr)Player(_ctx, _idx, ud);
	return ptr;
}

void PlayerMgr::releasePlayer(Player **self) {
	(*self)->~Player();
	(*self) = nullptr;
	_size++;
}