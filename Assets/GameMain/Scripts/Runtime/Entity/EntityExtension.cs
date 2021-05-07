//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityGameFramework.Runtime;
using GameFramework.DataTable;
using Sirius.Runtime;
using System;

namespace Sirius.Runtime
{
    public static class EntityExtension
    {
        //显示实体
        public static void ShowHotEntity(this EntityComponent entityComponent, string hotLogicTypeName, string entityGroup, int priority, int entityId, int dataId, object userData)
        {
            if (entityId == 0 || dataId == 0)
            {
                Log.Warning("[EntityExtension.ShowRuntimeEntity] entityId or dataId is invalid.");
                return;
            }

            IDataTable<DREntity> dtEntity = GameEntry.DataTable.GetDataTable<DREntity>();
            DREntity drEntity = dtEntity.GetDataRow(dataId);
            if (drEntity == null)
            {
                Log.Warning("Can not load entity id '{0}' from data table.", dataId.ToString());
                return;
            }

            //实体传递的数据
            UserEntityData entityData = new UserEntityData(hotLogicTypeName, userData);
            entityComponent.ShowEntity(entityId, typeof(HotEntity), AssetUtility.GetEntityAsset(drEntity.AssetName), entityGroup, priority, entityData);
        }

        //隐藏逻辑实体
        public static void HideEntity(this EntityComponent entityComponent, Entity entity)
        {
            entityComponent.HideEntity(entity);
        }

        //附加实体
        public static void AttachEntity(this EntityComponent entityComponent, Entity entity, int ownerId, string parentTransformPath = null, object userData = null)
        {
            entityComponent.AttachEntity(entity, ownerId, parentTransformPath, userData);
        }
    }
}
