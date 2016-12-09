#include "stdafx.h"
#include "Player.h"
#include "Context.h"

Player::Player() 
	: _ctx(nullptr)
	, _id(-1)
	, _role(nullptr)
	, _ud(nullptr)
{}

Player::Player(Context *ctx, int id, void *ud)
	: _ctx(ctx)
	, _id(id)
	, _role(nullptr)
	, _ud(ud)
{
	RoleMgr *roleMgr = _ctx->getRoleMgr();
	Role *role = roleMgr->createRole(this);
	_role = role;
}

Player::~Player() {
	RoleMgr *roleMgr = _ctx->getRoleMgr();
	roleMgr->releaseRole(&_role);
}
