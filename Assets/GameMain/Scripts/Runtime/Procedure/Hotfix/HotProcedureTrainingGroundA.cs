namespace Sirius.Runtime 
{	
	//训练场景A
	public class HotProcedureTrainingGroundA : HotProcedure
	{
	    public override bool UseNativeDialog { get { return false; } }

        private string m_HotProcedureLogicTypeFullName = "ProcedureTrainingGroundA".HotFixTypeFullName();
        public override string HotProcedureLogicTypeFullName { get { return m_HotProcedureLogicTypeFullName; } }

	}
}
