#ifndef SHARPC_H
#define SHARPC_H

#ifdef __cplusplus
extern "C" {
#endif // __cplusplus
#include "config.h"
#include <stdint.h>

typedef enum CSType {
	NIL = 0,
	INT32 = 1,
	INT64 = 2,
	REAL = 3,
	BOOLEAN = 4,
	STRING = 5,
	INTPTR = 6,
	SHARPOBJECT = 7,
	SHARPFUNCTION = 8,
	SHARPSTRING = 9,
} CSType;

typedef struct CSObject {
	//public WeakReference obj { get; set; }

	CSType  type;
	void *  ptr;
	int32_t v32; // len or key or d
	int64_t v64;
	double  f;
} CSObject;




typedef int(*sharp_callback)(int argc, struct CSObject *argv, int res);

struct sharpc {
	int reference;
	sharp_callback sharpcall;
};

/*
** @brief C#���ôκ�������һ������
**
*/
SHARPC_API struct sharpc*
sharpc_create(sharp_callback cb);

SHARPC_API void
sharpc_retain(struct sharpc *self);

SHARPC_API void
sharpc_release(struct sharpc *self);

/*
** @breif �ص�sharp����
** @param argc CSObject ����
** @param 
** @return 0 ��ȷ�� 1 �Ժ���󣬴���
 */
int
sharpc_callsharp(struct sharpc *self, int argc, struct CSObject *argv, int res);

/*
** @breif ����c����
** @param argc CSObject ����
** @param
** @return 0 ��ȷ�� 1 �Ժ���󣬴���
*/
SHARPC_API int
sharpc_callc(struct sharpc *self, int argc, struct CSObject *argv, int res);

void
sharpc_log(struct sharpc *self, struct CSObject xx[2]);

#ifdef __cplusplus
}
#endif // __cplusplus
#endif // !SHARPC_H
