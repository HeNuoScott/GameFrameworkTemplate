namespace Sirius.Runtime 
{	
	//训练场景B
	public class HotProcedureTrainingGroundB : HotProcedure
	{
	    public override bool UseNativeDialog { get { return false; } }

        private string m_HotProcedureLogicTypeFullName = "ProcedureTrainingGroundB".HotFixTypeFullName();
        public override string HotProcedureLogicTypeFullName { get { return m_HotProcedureLogicTypeFullName; } }
	}
}
