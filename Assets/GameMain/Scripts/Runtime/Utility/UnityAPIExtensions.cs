// -----------------------------------------------
// Copyright © Sirius. All rights reserved.
// CreateTime: 2021/4/26   10:35:34
// -----------------------------------------------

using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace Sirius.Runtime
{
    public static class APIExtension
    {
        public static GameObject Show(this GameObject selfObj)
        {
            selfObj.SetActive(true);
            return selfObj;
        }
        public static GameObject Hide(this GameObject selfObj)
        {
            selfObj.SetActive(false);
            return selfObj;
        }
        //获取父节点下面所有的 Transform 组件。
        static Transform[] GetAllTransform(Transform transform)
        {
            Transform[] allTransform = transform.GetComponentsInChildren<Transform>(true);

            return allTransform;
        }
        public static Transform GetTransform(this Transform transform, string tranName)
        {
            Transform[] allTransform = GetAllTransform(transform);
            for (int i = 0; i < allTransform.Length; i++)
            {
                Transform item = allTransform[i];
                if (item.name == tranName)
                {
                    return item;
                }
            }
            return null;
        }
        public static GameObject GetGameObject(this Transform transform, string objName)
        {
            Transform[] allTransform = GetAllTransform(transform);
            for (int i = 0; i < allTransform.Length; i++)
            {
                Transform item = allTransform[i];
                if (item.name == objName)
                {
                    return item.gameObject;
                }
            }
            return null;
        }
        public static GameObject Clone(Transform prefab, Transform parent, string objectName = null)
        {
            GameObject mCloneObj = GameObject.Instantiate(prefab).gameObject;

            if (objectName != null)
            {
                mCloneObj.name = objectName;
            }

            mCloneObj.transform.SetParent(parent, false);

            return mCloneObj;
        }
        public static GameObject Clone(this GameObject prefab, Transform parent, string objectName = null)
        {
            GameObject mCloneObj = GameObject.Instantiate(prefab).gameObject;

            if (objectName != null)
            {
                mCloneObj.name = objectName;
            }

            mCloneObj.transform.SetParent(parent, false);

            return mCloneObj;
        }

        #region Transform Extension
        /// <summary>
        /// 缓存的一些变量,免得每次声明
        /// </summary>
        private static Vector3 mLocalPos;
        private static Vector3 mScale;
        private static Vector3 mPos;

        public static Vector3 GetLocalPosition(this Transform transform)
        {
            return transform.localPosition;
        }
        public static Quaternion GetLocalRotation(this Transform transform)
        {
            return transform.localRotation;
        }
        public static Quaternion GetRotation(this Transform transform)
        {
            return transform.rotation;
        }
        public static Vector3 GetLocalScale(this Transform transform)
        {
            return transform.localScale;
        }
        public static Vector3 GetPosition(this Transform transform)
        {
            return transform.position;
        }
        public static Vector3 GetGlobalScale(this Transform transform)
        {
            return transform.lossyScale;
        }
        public static Vector3 GetScale(this Transform transform)
        {
            return transform.lossyScale;
        }
        public static Vector3 GetWorldScale(this Transform transform)
        {
            return transform.lossyScale;
        }
        public static Vector3 GetLossyScale(this Transform transform)
        {
            return transform.lossyScale;
        }

        public static Transform Parent(this Transform transform, Transform parent) 
        {
            transform.SetParent(parent == null ? null : transform);
            return transform;
        }
        public static Transform AsRootTransform(this Transform transform)
        {
            transform.SetParent(null);
            return transform;
        } 
        public static Transform LocalIdentity(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
            return transform;
        }
        public static Transform LocalPosition(this Transform transform, Vector3 localPos)
        {
            transform.localPosition = localPos;
            return transform;
        }
        public static Transform LocalPosition(this Transform transform, float x, float y, float z)
        {
            transform.localPosition = new Vector3(x, y, z);
            return transform;
        }
        public static Transform LocalPosition(this Transform transform, float x, float y)
        {
            mLocalPos = transform.localPosition;
            mLocalPos.x = x;
            mLocalPos.y = y;
            transform.localPosition = mLocalPos;
            return transform;
        }
        public static Transform LocalPositionX(this Transform transform, float x)
        {
            mLocalPos = transform.localPosition;
            mLocalPos.x = x;
            transform.localPosition = mLocalPos;
            return transform;
        }
        public static Transform LocalPositionY(this Transform transform, float y)
        {
            mLocalPos = transform.localPosition;
            mLocalPos.y = y;
            transform.localPosition = mLocalPos;
            return transform;
        }
        public static Transform LocalPositionZ(this Transform transform, float z)
        {
            mLocalPos = transform.localPosition;
            mLocalPos.z = z;
            transform.localPosition = mLocalPos;
            return transform;
        }
        public static Transform LocalPositionIdentity(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            return transform;
        }
        public static Transform LocalRotation(this Transform transform, Quaternion localRotation)
        {
            transform.localRotation = localRotation;
            return transform;
        }
        public static Transform LocalRotationIdentity(this Transform transform)
        {
            transform.localRotation = Quaternion.identity;
            return transform;
        }
        public static Transform LocalScale(this Transform transform, Vector3 scale)
        {
            transform.localScale = scale;
            return transform;
        }
        public static Transform LocalScale(this Transform transform, float xyz)
        {
            transform.localScale = Vector3.one * xyz;
            return transform;
        }
        public static Transform LocalScale(this Transform transform, float x, float y, float z)
        {
            mScale = transform.localScale;
            mScale.x = x;
            mScale.y = y;
            mScale.z = z;
            transform.localScale = mScale;
            return transform;
        }
        public static Transform LocalScale(this Transform transform, float x, float y)
        {
            mScale = transform.localScale;
            mScale.x = x;
            mScale.y = y;
            transform.localScale = mScale;
            return transform;
        }
        public static Transform LocalScaleX(this Transform transform, float x)
        {
            mScale = transform.localScale;
            mScale.x = x;
            transform.localScale = mScale;
            return transform;
        }
        public static Transform LocalScaleY(this Transform transform, float y)
        {
            mScale = transform.localScale;
            mScale.y = y;
            transform.localScale = mScale;
            return transform;
        }
        public static Transform LocalScaleZ(this Transform transform, float z)
        {
            mScale = transform.localScale;
            mScale.z = z;
            transform.localScale = mScale;
            return transform;
        }
        public static Transform LocalScaleIdentity(this Transform transform)
        {
            transform.localScale = Vector3.one;
            return transform;
        }
        public static Transform Identity(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
            return transform;
        }
        public static Transform Position(this Transform transform, Vector3 position)
        {
            transform.position = position;
            return transform;
        }
        public static Transform Position(this Transform transform, float x, float y, float z)
        {
            transform.position = new Vector3(x, y, z);
            return transform;
        }
        public static Transform Position(this Transform transform, float x, float y)
        {
            mPos = transform.position;
            mPos.x = x;
            mPos.y = y;
            transform.position = mPos;
            return transform;
        }
        public static Transform PositionIdentity(this Transform transform)
        {
            transform.position = Vector3.zero;
            return transform;
        }
        public static Transform PositionX(this Transform transform, float x)
        {
            mPos = transform.position;
            mPos.x = x;
            transform.position = mPos;
            return transform;
        }
        public static Transform PositionX(this Transform transform, Func<float, float> xSetter)
        {
            mPos = transform.position;
            mPos.x = xSetter(mPos.x);
            transform.position = mPos;
            return transform;
        }
        public static Transform PositionY(this Transform transform, float y)
        {
            mPos = transform.position;
            mPos.y = y;
            transform.position = mPos;
            return transform;
        }
        public static Transform PositionY(this Transform transform, Func<float, float> ySetter)
        {
            mPos = transform.position;
            mPos.y = ySetter(mPos.y);
            transform.position = mPos;
            return transform;
        }
        public static Transform PositionZ(this Transform transform, float z)
        {
            mPos = transform.position;
            mPos.z = z;
            transform.position = mPos;
            return transform;
        }
        public static Transform PositionZ(this Transform transform, Func<float, float> zSetter)
        {
            mPos = transform.position;
            mPos.z = zSetter(mPos.z);
            transform.position = mPos;
            return transform;
        }
        public static Transform RotationIdentity(this Transform transform)
        {
            transform.rotation = Quaternion.identity;
            return transform;
        }
        public static Transform Rotation(this Transform transform, Quaternion rotation)
        {
            transform.rotation = rotation;
            return transform;
        }
        public static Transform DestroyAllChild(this Transform transform)
        {
            var childCount = transform.childCount;
            for (var i = 0; i < childCount; i++)
            {
                UnityEngine.Object.Destroy(transform.GetChild(0).gameObject);
            }
            return transform;
        }
        public static Transform AsLastSibling(this Transform transform)
        {
            transform.SetAsLastSibling();
            return transform;
        }

        public static Transform AsFirstSibling(this Transform transform)
        {
            transform.SetAsFirstSibling();
            return transform;
        }

        public static Transform SiblingIndex(this Transform transform, int index)
        {
            transform.SetSiblingIndex(index);
            return transform;
        }


        public static Transform FindByPath(this Transform selfTrans, string path)
        {
            return selfTrans.Find(path.Replace(".", "/"));
        }
        public static Transform SeekTrans(this Transform selfTransform, string uniqueName)
        {
            var childTrans = selfTransform.Find(uniqueName);

            if (null != childTrans)
                return childTrans;

            foreach (Transform trans in selfTransform)
            {
                childTrans = trans.SeekTrans(uniqueName);

                if (null != childTrans)
                    return childTrans;
            }

            return null;
        }
        public static void CopyDataFromTrans(this Transform selfTrans, Transform fromTrans)
        {
            selfTrans.SetParent(fromTrans.parent);
            selfTrans.localPosition = fromTrans.localPosition;
            selfTrans.localRotation = fromTrans.localRotation;
            selfTrans.localScale = fromTrans.localScale;
        }
        /// <summary>
        /// 递归遍历子物体，并调用函数
        /// </summary>
        /// <param name="tfParent"></param>
        /// <param name="action"></param>
        public static void ActionRecursion(this Transform tfParent, Action<Transform> action)
        {
            action(tfParent);
            foreach (Transform tfChild in tfParent)
            {
                tfChild.ActionRecursion(action);
            }
        }
        /// <summary>
        /// 递归遍历查找指定的名字的子物体
        /// </summary>
        /// <param name="tfParent">当前Transform</param>
        /// <param name="name">目标名</param>
        /// <param name="stringComparison">字符串比较规则</param>
        /// <returns></returns>
        public static Transform FindChildRecursion(this Transform tfParent, string name,StringComparison stringComparison = StringComparison.Ordinal)
        {
            if (tfParent.name.Equals(name, stringComparison))
            {
                //Debug.Log("Hit " + tfParent.name);
                return tfParent;
            }

            foreach (Transform tfChild in tfParent)
            {
                Transform tfFinal = null;
                tfFinal = tfChild.FindChildRecursion(name, stringComparison);
                if (tfFinal)
                {
                    return tfFinal;
                }
            }

            return null;
        }
        /// <summary>
        /// 递归遍历查找相应条件的子物体
        /// </summary>
        /// <param name="tfParent">当前Transform</param>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        public static Transform FindChildRecursion(this Transform tfParent, Func<Transform, bool> predicate)
        {
            if (predicate(tfParent))
            {
                Debug.Log("Hit " + tfParent.name);
                return tfParent;
            }

            foreach (Transform tfChild in tfParent)
            {
                Transform tfFinal = null;
                tfFinal = tfChild.FindChildRecursion(predicate);
                if (tfFinal)
                {
                    return tfFinal;
                }
            }

            return null;
        }
        public static string GetPath(this Transform transform)
        {
            var sb = new System.Text.StringBuilder();
            var t = transform;
            while (true)
            {
                sb.Insert(0, t.name);
                t = t.parent;
                if (t)
                {
                    sb.Insert(0, "/");
                }
                else
                {
                    return sb.ToString();
                }
            }
        }
        #endregion

        #region RectTransformExtension
        public static Vector2 GetPosInRootTrans(this RectTransform selfRectTransform, Transform rootTrans)
        {
            return RectTransformUtility.CalculateRelativeRectTransformBounds(rootTrans, selfRectTransform).center;
        }
        public static RectTransform AnchorPosX(this RectTransform selfRectTrans, float anchorPosX)
        {
            var anchorPos = selfRectTrans.anchoredPosition;
            anchorPos.x = anchorPosX;
            selfRectTrans.anchoredPosition = anchorPos;
            return selfRectTrans;
        }
        public static RectTransform AnchorPosY(this RectTransform selfRectTrans, float anchorPosY)
        {
            var anchorPos = selfRectTrans.anchoredPosition;
            anchorPos.y = anchorPosY;
            selfRectTrans.anchoredPosition = anchorPos;
            return selfRectTrans;
        }
        public static RectTransform SetSizeWidth(this RectTransform selfRectTrans, float sizeWidth)
        {
            var sizeDelta = selfRectTrans.sizeDelta;
            sizeDelta.x = sizeWidth;
            selfRectTrans.sizeDelta = sizeDelta;
            return selfRectTrans;
        }
        public static RectTransform SetSizeHeight(this RectTransform selfRectTrans, float sizeHeight)
        {
            var sizeDelta = selfRectTrans.sizeDelta;
            sizeDelta.y = sizeHeight;
            selfRectTrans.sizeDelta = sizeDelta;
            return selfRectTrans;
        }
        public static Vector2 GetWorldSize(this RectTransform selfRectTrans)
        {
            return RectTransformUtility.CalculateRelativeRectTransformBounds(selfRectTrans).size;
        }
        #endregion

        #region UGUI Extension
        //button
        public static Button GetButton(this Transform transform, string btnName)
        {
            Transform[] allTransform = GetAllTransform(transform);
            for (int i = 0; i < allTransform.Length; i++)
            {
                Transform item = allTransform[i];
                if (item.name == btnName)
                {
                    return item.GetComponent<Button>();
                }
            }
            return null;
        }
        public static Button GetButton(this GameObject gameObject, string btnName)
        {
            Transform[] allTransform = GetAllTransform(gameObject.transform);
            for (int i = 0; i < allTransform.Length; i++)
            {
                Transform item = allTransform[i];
                if (item.name == btnName)
                {
                    return item.GetComponent<Button>();
                }
            }
            return null;
        }
        public static Button GetButton(this Transform transform)
        {
            return transform.GetComponent<Button>();
        }
        public static Button GetButton(this GameObject gameObject)
        {
            return gameObject.GetComponent<Button>();
        }
        //Text
        public static Text GetText(this Transform transform, string txtName)
        {
            Transform[] allTransform = GetAllTransform(transform);
            for (int i = 0; i < allTransform.Length; i++)
            {
                Transform item = allTransform[i];
                if (item.name == txtName)
                {
                    return item.GetComponent<Text>();
                }
            }
            return null;
        }
        public static Text GetText(this GameObject gameObject, string txtName)
        {
            Transform[] allTransform = GetAllTransform(gameObject.transform);
            for (int i = 0; i < allTransform.Length; i++)
            {
                Transform item = allTransform[i];
                if (item.name == txtName)
                {
                    return item.GetComponent<Text>();
                }
            }
            return null;
        }
        public static Text GetText(this Transform transform)
        {
            return transform.GetComponent<Text>();
        }
        public static Text GetText(this GameObject gameObject)
        {
            return gameObject.GetComponent<Text>();
        }
        //Image
        public static Image GetImage(this Transform transform, string imgName)
        {
            Transform[] allTransform = GetAllTransform(transform);
            for (int i = 0; i < allTransform.Length; i++)
            {
                Transform item = allTransform[i];
                if (item.name == imgName)
                {
                    return item.GetComponent<Image>();
                }
            }
            return null;
        }
        public static Image GetImage(this GameObject gameObject, string imgName)
        {
            Transform[] allTransform = GetAllTransform(gameObject.transform);
            for (int i = 0; i < allTransform.Length; i++)
            {
                Transform item = allTransform[i];
                if (item.name == imgName)
                {
                    return item.GetComponent<Image>();
                }
            }
            return null;
        }
        public static Image GetImage(this Transform transform)
        {
            return transform.GetComponent<Image>();
        }
        public static Image GetImage(this GameObject gameObject)
        {
            return gameObject.GetComponent<Image>();
        }
        public static Image FillAmount(this Image selfImage, float fillamount)
        {
            selfImage.fillAmount = fillamount;
            return selfImage;
        }
        //InputField
        public static InputField GetInputField(this Transform transform, string InputFieldName)
        {
            Transform[] allTransform = GetAllTransform(transform);
            for (int i = 0; i < allTransform.Length; i++)
            {
                Transform item = allTransform[i];
                if (item.name == InputFieldName)
                {
                    return item.GetComponent<InputField>();
                }
            }
            return null;
        }
        public static InputField GetInputField(this GameObject gameObject, string InputFieldName)
        {
            Transform[] allTransform = GetAllTransform(gameObject.transform);
            for (int i = 0; i < allTransform.Length; i++)
            {
                Transform item = allTransform[i];
                if (item.name == InputFieldName)
                {
                    return item.GetComponent<InputField>();
                }
            }
            return null;
        }
        public static InputField GetInputField(this Transform transform)
        {
            return transform.GetComponent<InputField>();
        }
        public static InputField GetInputField(this GameObject gameObject)
        {
            return gameObject.GetComponent<InputField>();
        }
        //Toogle
        public static Toggle GetToggle(this Transform transform, string toggleName)
        {
            Transform[] allTransform = GetAllTransform(transform);
            for (int i = 0; i < allTransform.Length; i++)
            {
                Transform item = allTransform[i];
                if (item.name == toggleName)
                {
                    return item.GetComponent<Toggle>();
                }
            }
            return null;
        }
        public static Toggle GetToggle(this GameObject gameObject, string toggleName)
        {
            Transform[] allTransform = GetAllTransform(gameObject.transform);
            for (int i = 0; i < allTransform.Length; i++)
            {
                Transform item = allTransform[i];
                if (item.name == toggleName)
                {
                    return item.GetComponent<Toggle>();
                }
            }
            return null;
        }
        public static Toggle GetToggle(this Transform transform)
        {
            return transform.GetComponent<Toggle>();
        }
        public static Toggle GetToggle(this GameObject gameObject)
        {
            return gameObject.GetComponent<Toggle>();
        }
        //ToggleGroup
        public static ToggleGroup GetToggleGroup(this Transform transform, string toggleGroupName)
        {
            Transform[] allTransform = GetAllTransform(transform);
            for (int i = 0; i < allTransform.Length; i++)
            {
                Transform item = allTransform[i];
                if (item.name == toggleGroupName)
                {
                    return item.GetComponent<ToggleGroup>();
                }
            }
            return null;
        }
        public static ToggleGroup GetToggleGroup(this GameObject gameObject, string toggleGroupName)
        {
            Transform[] allTransform = GetAllTransform(gameObject.transform);
            for (int i = 0; i < allTransform.Length; i++)
            {
                Transform item = allTransform[i];
                if (item.name == toggleGroupName)
                {
                    return item.GetComponent<ToggleGroup>();
                }
            }
            return null;
        }
        public static ToggleGroup GetToggleGroup(this Transform transform)
        {
            return transform.GetComponent<ToggleGroup>();
        }
        public static ToggleGroup GetToggleGroup(this GameObject gameObject)
        {
            return gameObject.GetComponent<ToggleGroup>();
        }
        public static void RegOnValueChangedEvent(this Toggle selfToggle, UnityAction<bool> onValueChangedEvent)
        {
            selfToggle.onValueChanged.AddListener(onValueChangedEvent);
        }
        //GridLayoutGroup
        public static GridLayoutGroup GetGridLayoutGroup(this Transform transform, string gridName)
        {
            Transform[] allTransform = GetAllTransform(transform);
            for (int i = 0; i < allTransform.Length; i++)
            {
                Transform item = allTransform[i];
                if (item.name == gridName)
                {
                    return item.GetComponent<GridLayoutGroup>();
                }
            }
            return null;
        }
        public static GridLayoutGroup GetGridLayoutGroup(this GameObject gameObject, string gridName)
        {
            Transform[] allTransform = GetAllTransform(gameObject.transform);
            for (int i = 0; i < allTransform.Length; i++)
            {
                Transform item = allTransform[i];
                if (item.name == gridName)
                {
                    return item.GetComponent<GridLayoutGroup>();
                }
            }
            return null;
        }
        public static GridLayoutGroup GetGridLayoutGroup(this Transform transform)
        {
            return transform.GetComponent<GridLayoutGroup>();
        }
        public static GridLayoutGroup GetGridLayoutGroup(this GameObject gameObject)
        {
            return gameObject.GetComponent<GridLayoutGroup>();
        }
        //Animator
        public static Animator GetAnimator(this Transform transform, string animatorName)
        {
            Transform[] allTransform = GetAllTransform(transform);
            for (int i = 0; i < allTransform.Length; i++)
            {
                Transform item = allTransform[i];
                if (item.name == animatorName)
                {
                    return item.GetComponent<Animator>();
                }
            }
            return null;
        }
        public static Animator GetAnimator(this GameObject gameObject, string animatorName)
        {
            Transform[] allTransform = GetAllTransform(gameObject.transform);
            for (int i = 0; i < allTransform.Length; i++)
            {
                Transform item = allTransform[i];
                if (item.name == animatorName)
                {
                    return item.GetComponent<Animator>();
                }
            }
            return null;
        }
        public static Animator GetAnimator(this Transform transform)
        {
            return transform.GetComponent<Animator>();
        }
        public static Animator GetAnimator(this GameObject gameObject)
        {
            return gameObject.GetComponent<Animator>();
        }
        #endregion
    }
}