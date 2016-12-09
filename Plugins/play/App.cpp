#include "stdafx.h"
#include "App.h"
#include <iostream>
#include <foundation/Px.h>
#include <common/PxTolerancesScale.h>
#include <extensions/PxExtensionsAPI.h>

using namespace physx;

App::App()
	: _exit(false) {

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

	_a = _physics->createRigidDynamic(PxTransform(50.0f, 120.0f, 50.0f));
	PxVec3 dimensions(1.0f, 1.0f, 1.0f);
	_a->createShape(PxBoxGeometry(dimensions), *material);
	_scene->addActor(*_a);

}


App::~App() {
	_foundation->release();
}


void App::run() {
	while (!_exit) {
		update();
	}
}

void App::update() {
	std::cout << "please enter c:" << std::endl;
	char c;
	std::cin >> c;
	switch (c) {
	case 'n':
		break;
	case 'e':
		_exit = true;
		break;
	default:
		break;
	}
	_scene->simulate(2.0f);
	_scene->fetchResults(true);
	PxTransform trans = _a->getGlobalPose();
	std::cout << trans.p.x << " " << trans.p.y << " " << trans.p.z << std::endl;
}
