using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{

    [Serializable] public class LearnerComponentsSetting : BaseEntity
    {
        public LearnerComponentsSetting()
        { }

        public new enum ListMethod
        {
            GetAll,
            GetAllLearner,
            UpdateLearnerComponent
        }

        public new enum Method
        {
           UpdateLearnerComponent
        }

        private string _ComponentID;
        public string ComponentID
        {
            get { return _ComponentID; }
            set { if (this._ComponentID != value) { _ComponentID = value; } }
        }

        private string _ComponentName;
        public string ComponentName
        {
            get { return _ComponentName; }
            set { if (this._ComponentName != value) { _ComponentName = value; } }
        }

        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set { if (this._isVisible != value) { _isVisible = value; } }
        }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { if (this._isActive != value) { _isActive = value; } }
        }

        private string _FeatureId;
        public string FeatureId
        {
            get { return _FeatureId; }
            set { if (this._FeatureId != value) { _FeatureId = value; } }
        }

        private int _TotalCount;
        public int TotalCount
        {
            get { return _TotalCount; }
            set { if (this._TotalCount != value) { _TotalCount = value; } }
        }
    }

    
}
