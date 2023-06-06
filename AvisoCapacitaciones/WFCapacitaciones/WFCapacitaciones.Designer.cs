using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace AvisoCapacitaciones.WFCapacitaciones
{
    public sealed partial class WFCapacitaciones
    {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        private void InitializeComponent()
        {
            this.CanModifyActivities = true;
            System.Workflow.Runtime.CorrelationToken correlationtoken1 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            this.onWorkflowActivated1 = new Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated();
            this.eventDrivenActivity1 = new System.Workflow.Activities.EventDrivenActivity();
            this.WFCapacitacionesInitialState = new System.Workflow.Activities.StateActivity();
            // 
            // onWorkflowActivated1
            // 
            correlationtoken1.Name = "workflowToken";
            correlationtoken1.OwnerActivityName = "WFCapacitaciones";
            this.onWorkflowActivated1.CorrelationToken = correlationtoken1;
            this.onWorkflowActivated1.EventName = "OnWorkflowActivated";
            this.onWorkflowActivated1.Name = "onWorkflowActivated1";
            this.onWorkflowActivated1.WorkflowId = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind1.Name = "WFCapacitaciones";
            activitybind1.Path = "workflowProperties";
            this.onWorkflowActivated1.SetBinding(Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated.WorkflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // eventDrivenActivity1
            // 
            this.eventDrivenActivity1.Activities.Add(this.onWorkflowActivated1);
            this.eventDrivenActivity1.Name = "eventDrivenActivity1";
            // 
            // WFCapacitacionesInitialState
            // 
            this.WFCapacitacionesInitialState.Activities.Add(this.eventDrivenActivity1);
            this.WFCapacitacionesInitialState.Name = "WFCapacitacionesInitialState";
            // 
            // WFCapacitaciones
            // 
            this.Activities.Add(this.WFCapacitacionesInitialState);
            this.CompletedStateName = null;
            this.DynamicUpdateCondition = null;
            this.InitialStateName = "WFCapacitacionesInitialState";
            this.Name = "WFCapacitaciones";
            this.CanModifyActivities = false;

        }

        #endregion

        private Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated onWorkflowActivated1;
        private EventDrivenActivity eventDrivenActivity1;
        private StateActivity WFCapacitacionesInitialState;
    }
}
