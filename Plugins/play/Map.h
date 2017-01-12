#ifndef __MAP_H_
#define __MAP_H_

#include "Context.h"

#include <PxRigidStatic.h>

#include <list>

class Map {
public:
	Map(Context *ctx, Scene *scene);
	~Map();

	void createFloor();

private:
	Context *_ctx;
	Scene   *_scene;

	std::list<physx::PxRigidStatic*> _list;
};

#endif // !__MAP_H_


