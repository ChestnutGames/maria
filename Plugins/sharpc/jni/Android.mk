LOCAL_PATH := $(call my-dir)
include $(CLEAR_VARS)
LOCAL_MODULE := crypt
LOCAL_MODULE_FILENAME := libsharpc
LOCAL_SRC_FILES := ./../log.c \
                   ./../sharpc.c \
		   ./../../rudp/rudp.c \
                   ./../../rudp/rudp_aux.c
LOCAL_C_INCLUDES := $(LOCAL_PATH)/../ \
		$(LOCAL_PATH)/../../rudp/
           
include $(BUILD_SHARED_LIBRARY)