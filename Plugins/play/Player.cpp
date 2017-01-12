#include "stdafx.h"
#include "Player.h"
#include "Context.h"

#include <PxRigidDynamic.h>

using namespace physx;

Player::Player() 
	: _ctx(nullptr)
	, _next(nullptr)
	, _uid(-1)
	, _sid(-1)
	, _rigid(nullptr)
{}

Player::Player(Context *ctx)
	: _ctx(ctx)
	, _next(nullptr)
	, _uid(-1)
	, _sid(-1)
	, _rigid(nullptr)
{
}

Player::~Player() {
}

void Player::createRigid() {
	if (_rigid == nullptr) {
		physx::PxTransform trans = physx::PxTransform::createIdentity();
		trans.transform(PxVec3(0, 50, 0));
		physx::PxRigidDynamic *rigid = _ctx->getPhysics()->createRigidDynamic(trans);
		rigid->setMass(100);
		//rigid->setRigidBodyFlag(physx::PxRigidBodyFlag::eKINEMATIC, true);
		_rigid = rigid;
		_rigid->userData = this;

		physx::PxBoxGeometry g(10, 10, 10);
		physx::PxMaterial *m = _ctx->getPhysics()->createMaterial(0.5f, 0.5f, 0.1f);
		physx::PxShape *box = rigid->createShape(g, *m, physx::PxTransform::createIdentity());
		//_rigid->attachShape(*box);
	}
}

void Player::releaseRigid() {
	if (_rigid != nullptr) {
	}
}