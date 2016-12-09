#ifndef __APP_H_
#define __APP_H_

#include "stdafx.h"
#include "Context.h"

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

#include <map>

class PLAY_API App {
public:
	App();
	~App();

	void run();

	void updata();
	int join(void *ud);
	void leave(int id);

private:
	bool                      _exit;
	Context                   _ctx;

};

#endif
