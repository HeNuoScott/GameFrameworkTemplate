
namespace Sirius.Runtime 
{	
	//大厅
	public class HotProcedureLobby : HotProcedure
	{
	    public override bool UseNativeDialog { get { return false; } }

        private string m_HotProcedureLogicTypeFullName = "ProcedureLobby".HotFixTypeFullName();
        public override string HotProcedureLogicTypeFullName { get { return m_HotProcedureLogicTypeFullName; } }
	}
}
