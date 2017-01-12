#include "stdafx.h"
#include "Map.h"
#include "Scene.h"

#include <PxRigidStatic.h>

Map::Map(Context *ctx, Scene *scene)
	: _ctx(ctx) 
	, _scene(scene)
{
	createFloor();
}


Map::~Map() {
}

void Map::createFloor() {
	physx::PxPhysics *physics = _ctx->getPhysics();

	physx::PxTransform trans = physx::PxTransform::createIdentity();
	trans.transform(physx::PxVec3(50, 0, 50));
	physx::PxRigidStatic *floor = physics->createRigidStatic(trans);
	_ctx->getScene()->getScene()->addActor(*floor);
	_list.push_back(floor);

	physx::PxBoxGeometry g(10, 10, 10);
	physx::PxPlaneGeometry 
	physx::PxMaterial *material = physics->createMaterial(0.5f, 0.5f, 0.1f);
	floor->createShape()
	/*physx::PxPlaneGeometry g()
	
	physx::PxShape *box = rigid->createShape(g, *m, physx::PxTransform::createIdentity());
	_rigid->attachShape(*box);*/

}