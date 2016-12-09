#include "stdafx.h"
#include "RoleMgr.h"
#include "Context.h"
#include <new>

RoleMgr::RoleMgr(Context *ctx)
	: _ctx(ctx)
	, _slots(nullptr)
	, _size(0)
	, _free(5)
	, _cap(6)
{
	_slots = new Role[_cap];
}

RoleMgr::~RoleMgr() {
	delete[] _slots;
}

Role * RoleMgr::createRole(Player *p) {
	if (_size == _cap) {
		return NULL;
	}
	if (_free < 0) {
		_free = _cap - 1;
	}
	Role *ptr = &_slots[_free];
	_free--;
	_size++;
	ptr = new (ptr)Role(_ctx, p);
	return ptr;
}

void RoleMgr::releaseRole(Role **self) {
	*self = nullptr;
	_size--;
}