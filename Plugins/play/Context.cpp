#include "stdafx.h"
#include "Context.h"
#include "PlayerMgr.h"
#include "Scene.h"

#include <foundation/Px.h>
#include <common/PxTolerancesScale.h>
#include <extensions/PxExtensionsAPI.h>

#include <cassert>
#include <iostream>

using namespace physx;

Context::Context() {
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
}

Context::~Context() {
	_foundation->release();

	if (_playerMgr) {
		delete _playerMgr;
	}
}

void Context::onInit() {
	_scene = new Scene(this);
	_playerMgr = new PlayerMgr(this);

	_scene->onInit();
}

void Context::update(float delta) {
	_scene->update(delta);
}