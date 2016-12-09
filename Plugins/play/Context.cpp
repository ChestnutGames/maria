#include "stdafx.h"
#include "Context.h"

#include <foundation/Px.h>
#include <common/PxTolerancesScale.h>
#include <extensions/PxExtensionsAPI.h>

#include <cassert>
#include <iostream>

using namespace physx;

Context::Context() {
	_playerMgr = new PlayerMgr(this);
	_roleMgr = new RoleMgr(this);

	physx::PxAllocatorCallback *allocator = &_allocator;
	_foundation = PxCreateFoundation(PX_PHYSICS_VERSION, *allocator, _error);
	if (!_foundation) {
	}
	_profileZoneManager = &physx::PxProfileZoneManager::createProfileZoneManager(_foundation);
	if (!_profileZoneManager) {
	}
	physx::PxTolerancesScale scale;
	_physics = PxCreatePhysics(PX_PHYSICS_VERSION, *_foundation, scale, _recordMem, _profileZoneManager);

	if (!PxInitExtensions(*_physics)) {
	}

	PxCookingParams params(scale);
	params.meshWeldTolerance = 0.001f;
	params.meshPreprocessParams = PxMeshPreprocessingFlags(PxMeshPreprocessingFlag::eWELD_VERTICES | PxMeshPreprocessingFlag::eREMOVE_UNREFERENCED_VERTICES | PxMeshPreprocessingFlag::eREMOVE_DUPLICATED_TRIANGLES);
	_cooking = PxCreateCooking(PX_PHYSICS_VERSION, *_foundation, params);

	PxMaterial *material = _physics->createMaterial(0.5f, 0.5f, 0.1f);
	physx::PxSceneDesc sceneDesc(_physics->getTolerancesScale());
	sceneDesc.gravity = PxVec3(0.0f, -9.81f, 0.0f);

	if (!sceneDesc.cpuDispatcher) {
		_dispatcher = PxDefaultCpuDispatcherCreate(1);
		if (!_dispatcher) {
		}
		sceneDesc.cpuDispatcher = _dispatcher;
	}

	if (!sceneDesc.filterShader) {
		sceneDesc.filterShader = PxDefaultSimulationFilterShader;
	}

	sceneDesc.flags |= PxSceneFlag::eENABLE_ACTIVETRANSFORMS;

	//sceneDesc.simulationOrder = PxSimulationOrder::eSOLVE_COLLIDE;

	_scene = _physics->createScene(sceneDesc);

	_plane = _physics->createRigidStatic(PxTransform(50.0f, 0.0f, 50.0f));
	PxShape *shape = _plane->createShape(PxPlaneGeometry(), *material);
	if (shape) {
		//shape->setLocalPose()
	}
	//_plane->attachShape(*shape);

}

Context::~Context() {
	_foundation->release();

	delete _playerMgr;
	delete _roleMgr;
}

int Context::addPlayer(Player *p) {
	assert(p != nullptr);
	int id = p->getId();
	_players[id] = p;
	/////////////////////////////
	Role *role = p->getRole();
	physx::PxRigidDynamic *rigid = role->getRigid();
	_scene->addActor(*rigid);

	return id;
}

void Context::removePlayer(int id) {
	Player *player = _players[id];
	Role *role = player->getRole();
	physx::PxRigidDynamic *rigid = role->getRigid();
	_scene->removeActor(*rigid);
	_players.erase(id);
}

Player * Context::getPlayer(int id) {
	return _players[id];
}

void Context::update() {
	_scene->simulate(2.0f);
	_scene->fetchResults(true);

	for (auto iter = _players.begin(); iter != _players.end(); iter++) {
		Role * role = (*iter).second->getRole();
		PxTransform trans = role->getRigid()->getGlobalPose();
		std::cout << trans.p.x << " " << trans.p.y << " " << trans.p.z << std::endl;
	}
}