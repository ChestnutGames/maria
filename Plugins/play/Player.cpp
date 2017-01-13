#include "stdafx.h"
#include "Player.h"
#include "Context.h"

#include <foundation/PxVec2.h>
#include <foundation/PxPlane.h>
#include <foundation/PxVec3.h>
#include <foundation/PxSimpleTypes.h>

#include <PxRigidDynamic.h>
#include <extensions/PxSimpleFactory.h>

using namespace physx;

void SetupDefaultRigidDynamic(PxRigidDynamic& body, bool kinematic = false) {
	body.setActorFlag(PxActorFlag::eVISUALIZATION, true);
	body.setAngularDamping(0.5f);
	body.setRigidBodyFlag(PxRigidBodyFlag::eKINEMATIC, kinematic);
}

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

		PxVec3 vel(0.0f, 0., 0.0f);
		physx::PxRigidDynamic *rigid = createBox(PxVec3(0, 50.0f, 0), PxVec3(10.0, 10.0f, 10.0f), &vel);
		_rigid = rigid;
		_rigid->userData = this;
		_rigid->setMass(100000.);
	}
}

void Player::releaseRigid() {
	if (_rigid != nullptr) {
	}
}

PxRigidDynamic* Player::createBox(const PxVec3& pos, const PxVec3& dims, const PxVec3* linVel, PxReal density) {
	PxPhysics  *physics  = _ctx->getPhysics();
	PxMaterial *material = _ctx->getDefaultMaterial();
	PxRigidDynamic* box = PxCreateDynamic(*physics, PxTransform(pos), PxBoxGeometry(dims), *material, density);
	PX_ASSERT(box);

	SetupDefaultRigidDynamic(*box);
	/*_ctx->getScene()->get ->addActor(*box);
	addPhysicsActors(box);*/

	if (linVel)
		box->setLinearVelocity(*linVel);

	return box;
}

///////////////////////////////////////////////////////////////////////////////

PxRigidDynamic* Player::createSphere(const PxVec3& pos, PxReal radius, const PxVec3* linVel, PxReal density) {

	PxPhysics  *physics = _ctx->getPhysics();
	PxMaterial *material = _ctx->getDefaultMaterial();
	PxRigidDynamic* sphere = PxCreateDynamic(*physics, PxTransform(pos), PxSphereGeometry(radius), *material, density);
	PX_ASSERT(sphere);

	SetupDefaultRigidDynamic(*sphere);
	/*mScene->addActor(*sphere);
	addPhysicsActors(sphere);*/

	if (linVel)
		sphere->setLinearVelocity(*linVel);

	//createRenderObjectsFromActor(sphere, material);
	return sphere;
}

///////////////////////////////////////////////////////////////////////////////

PxRigidDynamic* Player::createCapsule(const PxVec3& pos, PxReal radius, PxReal halfHeight, const PxVec3* linVel, PxReal density) {
	
	PxPhysics  *physics = _ctx->getPhysics();
	PxMaterial *material = _ctx->getDefaultMaterial();

	const PxQuat rot = PxQuat(PxIdentity);
	PX_UNUSED(rot);

	PxRigidDynamic* capsule = PxCreateDynamic(*physics, PxTransform(pos), PxCapsuleGeometry(radius, halfHeight), *material, density);
	PX_ASSERT(capsule);

	SetupDefaultRigidDynamic(*capsule);
	/*mScene->addActor(*capsule);
	addPhysicsActors(capsule);*/

	if (linVel)
		capsule->setLinearVelocity(*linVel);

	//createRenderObjectsFromActor(capsule, material);

	return capsule;
}

///////////////////////////////////////////////////////////////////////////////

PxRigidDynamic* Player::createConvex(const PxVec3& pos, const PxVec3* linVel, PxReal density) {
	
	/*PxConvexMesh* convexMesh = GenerateConvex(*mPhysics, *mCooking, getDebugConvexObjectScale(), false, true);
	PX_ASSERT(convexMesh);

	PxRigidDynamic* convex = PxCreateDynamic(*mPhysics, PxTransform(pos), PxConvexMeshGeometry(convexMesh), *mMaterial, density);
	PX_ASSERT(convex);

	SetupDefaultRigidDynamic(*convex);
	mScene->addActor(*convex);
	addPhysicsActors(convex);

	if (linVel)
		convex->setLinearVelocity(*linVel);

	createRenderObjectsFromActor(convex, material);

	return convex;*/
	return nullptr;
}

