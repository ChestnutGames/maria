#include "stdafx.h"
#include "Role.h"
#include "Context.h"
#include "Player.h"

#include <foundation/PxTransform.h>
#include <PxPhysics.h>

using namespace physx;

Role::Role()
	: _ctx(nullptr)
	, _player(nullptr)
	, _rigid(nullptr)
{}

Role::Role(Context *ctx, Player *p)
	: _ctx(ctx)
	, _player(p)
	, _rigid(nullptr)
{
	physx::PxPhysics *physics = ctx->getPhysics();
	PxMaterial *material = physics->createMaterial(0.5f, 0.5f, 0.1f);
	_rigid = physics->createRigidDynamic(PxTransform(50.0f, 120.0f, 50.0f));
	PxVec3 dimensions(1.0f, 1.0f, 1.0f);
	_rigid->createShape(PxBoxGeometry(dimensions), *material);
}

Role::~Role() {}
