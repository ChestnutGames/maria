LOCAL_PATH := $(call my-dir)
include $(CLEAR_VARS)
LOCAL_MODULE := play
LOCAL_MODULE_FILENAME := libplay

LOCAL_SRC_FILES := ./../sharpc/log.c \
		./../sharpc/sharpc.c \
		./../sharpc/play.cpp \
		./../rudp/rudp.c \
        ./../rudp/rudp_aux.c

LOCAL_C_INCLUDES := $(LOCAL_PATH)/../include \
		$(LOCAL_PATH)/../PxShared \
		$(LOCAL_PATH)/../PhysX
           
include $(BUILD_SHARED_LIBRARY)