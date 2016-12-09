#ifndef __APP_H_
#define __APP_H_

#include "stdafx.h"
#include <extensions/PxDefaultErrorCallback.h>
#include <extensions/PxDefaultAllocator.h>
#include <extensions/PxDefaultCpuDispatcher.h>
#include <foundation/PxAllocatorCallback.h>
#include <foundation/PxFoundation.h>
#include <physxprofilesdk/PxProfileZoneManager.h>
#include <cooking/PxCooking.h>
#include <PxPhysics.h>
#include <PxScene.h>
#include <PxActor.h>
#include <PxRigidDynamic.h>
#include <PxRigidStatic.h>
#include <PxMaterial.h>


class PLAY_API App {
public:
	App();
	~App();

	void run();
	void update();

private:
	bool _exit;
	bool                      _recordMem;
	physx::PxDefaultAllocator _allocator;
	physx::PxDefaultErrorCallback _error;
	physx::PxFoundation   *_foundation;

	physx::PxProfileZoneManager *_profileZoneManager;

	physx::PxPhysics      *_physics;
	physx::PxCooking      *_cooking;

	physx::PxDefaultCpuDispatcher *_dispatcher;

	physx::PxScene        *_scene;
	physx::PxRigidStatic     *_plane;
	physx::PxRigidDynamic    *_a;

};

#endif