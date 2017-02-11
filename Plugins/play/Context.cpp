#include "stdafx.h"
#include "Context.h"
#include "PlayerMgr.h"
#include "Scene.h"

#if defined(SHARPC)
#include "../sharpc/sharpc.h"
#include "../sharpc/log.h"
#else
#include "skynet.h"
#endif

#include <PxPhysicsAPI.h>
#include <extensions/PxExtensionsAPI.h>

#include <cassert>
#include <iostream>
#include <cstdarg>

using namespace physx;

Context::Context() {
	bool recordMemoryAllocations = true;
#ifdef ANDROID
	const bool useCustomTrackingAllocator = false;
#else
	const bool useCustomTrackingAllocator = true;
#endif // ANDROID

	physx::PxAllocatorCallback *allocator = &_allocator;
	if (useCustomTrackingAllocator) {
	}

	_foundation = PxCreateFoundation(PX_FOUNDATION_VERSION, *allocator, _error);
	if (!_foundation) {
		error("create foundation failture.");
	}

	physx::PxTolerancesScale scale;
	_physics = PxCreatePhysics(PX_PHYSICS_VERSION, *_foundation, scale, _recordMem, _pvd);

	if (_physics) {
		error("PxCreatePhysics failed.");
	}

	if (!PxInitExtensions(*_physics, _pvd)) {
		error("PxInitExtensions failed.");
	}

	PxCookingParams params(scale);
	params.meshWeldTolerance = 0.001f;
	params.meshPreprocessParams = PxMeshPreprocessingFlags(PxMeshPreprocessingFlag::eWELD_VERTICES);
	params.buildGPUData = true;

	_cooking = PxCreateCooking(PX_PHYSICS_VERSION, *_foundation, params);

	_physics->registerDeletionListener(*this, PxDeletionEventFlag::eUSER_RELEASE);

	_material = _physics->createMaterial(0.5f, 0.5f, 0.1f);
	if (!_material) {
		error("create Material failed.");
	}
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

#ifdef __cplusplus
extern "C" {
#endif // __cplusplus

void Context::info(const char *fmt, ...) {
	va_list ap;
	va_start(ap, fmt);
	char buffer[256] = { 0 };
	vsprintf(buffer, fmt, ap);
	va_end(ap);

#if defined(SHARPC)
	log_info(buffer);
#else
	// skynet_error(buffer)
#endif
}

void Context::warning(const char *fmt, ...) {
	va_list ap;
	va_start(ap, fmt);
	char buffer[256] = { 0 };
	vsprintf(buffer, fmt, ap);
	va_end(ap);

#if defined(SHARPC)
	log_warning(buffer);
#else
	// skynet_error(buffer);
#endif
}

void Context::error(const char *fmt, ...) {
	va_list ap;
	va_start(ap, fmt);
	char buffer[256] = { 0 };
	vsprintf(buffer, fmt, ap);
	va_end(ap);

#if defined(SHARPC)
	log_error(buffer);
#else
	// skynet_error(buffer);
#endif
}

#ifdef __cplusplus
}
#endif

void Context::onRelease(const PxBase* observed, void* userData, PxDeletionEventFlag::Enum deletionEvent) {
}