#include "stdafx.h"
#include "Map.h"
#include "Scene.h"

#include <PxRigidStatic.h>
#include <foundation/PxPlane.h>
#include <extensions/PxSimpleFactory.h>

using namespace physx;

Map::Map(Context *ctx, Scene *scene)
	: _ctx(ctx) 
	, _scene(scene)
{
	createGrid();
}


Map::~Map() {
}

void Map::createGrid() {
	physx::PxPhysics *physics = _ctx->getPhysics();
	physx::PxMaterial *material = _ctx->getDefaultMaterial();
	PxRigidStatic* plane = PxCreatePlane(*physics, PxPlane(PxVec3(0, 1, 0), 0), *material);
	if (!plane)
		printf("create plane failed!");

	//_scene->
	_scene->getScene()->addActor(*plane);
	
}