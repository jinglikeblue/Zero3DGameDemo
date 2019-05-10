using System;
using System.Collections.Generic;
using System.Reflection;

namespace ILRuntime.Runtime.Generated
{
    class CLRBindings
    {


        /// <summary>
        /// Initialize the CLR binding, please invoke this AFTER CLR Redirection registration
        /// </summary>
        public static void Initialize(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            UnityEngine_UI_Button_Binding.Register(app);
            UnityEngine_Events_UnityEvent_Binding.Register(app);
            GameKit_Joystick_Binding.Register(app);
            GameKit_Touchpad_Binding.Register(app);
            Zero_ASingletonMonoBehaviour_1_AudioPlayer_Binding.Register(app);
            Zero_ResMgr_Binding.Register(app);
            Zero_AudioPlayer_Binding.Register(app);
            System_Type_Binding.Register(app);
            Zero_ASingletonMonoBehaviour_1_ILBridge_Binding.Register(app);
            Zero_ILBridge_Binding.Register(app);
            UnityEngine_Vector2_Binding.Register(app);
            UnityEngine_Vector3_Binding.Register(app);
            CameraController_Binding.Register(app);
            UnityEngine_GameObject_Binding.Register(app);
            UnityEngine_Transform_Binding.Register(app);
            UnityEngine_Camera_Binding.Register(app);
            UnityEngine_Component_Binding.Register(app);
            UnityEngine_Quaternion_Binding.Register(app);
            UnityEngine_CharacterController_Binding.Register(app);
            UnityEngine_Animator_Binding.Register(app);
            DG_Tweening_ShortcutExtensions_Binding.Register(app);
            UnityEngine_UI_Image_Binding.Register(app);
            UnityEngine_WaitForSeconds_Binding.Register(app);
            System_NotSupportedException_Binding.Register(app);
            Zero_PointerClickEventListener_Binding.Register(app);
            UnityEngine_Debug_Binding.Register(app);
            Zero_GUILog_Binding.Register(app);
            System_DateTime_Binding.Register(app);
            System_String_Binding.Register(app);
            UnityEngine_UI_Toggle_Binding.Register(app);
            UnityEngine_Vector2Int_Binding.Register(app);
            UnityEngine_Application_Binding.Register(app);
            UnityEngine_QualitySettings_Binding.Register(app);
            UnityEngine_Screen_Binding.Register(app);
            UnityEngine_UI_Text_Binding.Register(app);
            System_Action_1_ILTypeInstance_Binding.Register(app);
            UnityEngine_MonoBehaviour_Binding.Register(app);
            UnityEngine_CanvasGroup_Binding.Register(app);
            UnityEngine_WaitForEndOfFrame_Binding.Register(app);
            UnityEngine_Object_Binding.Register(app);
            System_Collections_Generic_List_1_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_HashSet_1_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_List_1_ILTypeInstance_Binding_Enumerator_Binding.Register(app);
            System_IDisposable_Binding.Register(app);
            DG_Tweening_DOTween_Binding.Register(app);
            DG_Tweening_TweenSettingsExtensions_Binding.Register(app);
            DG_Tweening_DOTweenModuleUI_Binding.Register(app);
            System_Action_Binding.Register(app);
            Zero_GUIDeviceInfo_Binding.Register(app);
            System_Threading_Interlocked_Binding.Register(app);
            Zero_ComponentUtil_Binding.Register(app);
            Zero_ZeroView_Binding.Register(app);
            System_Activator_Binding.Register(app);
            System_Object_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_Dictionary_2_String_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Type_ILTypeInstance_Binding.Register(app);
            System_Threading_Monitor_Binding.Register(app);
            System_Action_2_ILTypeInstance_Object_Binding.Register(app);
            System_Action_1_Object_Binding.Register(app);

            ILRuntime.CLR.TypeSystem.CLRType __clrType = null;
        }

        /// <summary>
        /// Release the CLR binding, please invoke this BEFORE ILRuntime Appdomain destroy
        /// </summary>
        public static void Shutdown(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
        }
    }
}
