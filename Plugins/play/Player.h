#ifndef __PLAYER_H_
#define __PLAYER_H_

#include "Role.h"

class Context;
class Player {
public:
	Player();
	Player(Context *ctx, int id, void *ud);
	~Player();

	inline Role *getRole() const { return _role; }
	inline void setRole(Role *role) { _role = role; }

	inline void *getUd() const { return _ud; }
	inline void setUd(void *ud) { _ud = ud; }

	inline int getId() const { return _id; }

private:
	Context *_ctx;

	int   _id;
	Role *_role;
	void *_ud;
};

#endif