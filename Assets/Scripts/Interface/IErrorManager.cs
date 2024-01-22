public interface IErrorManager
{
	/// <summary>
	/// ���g�̃C���X�^���X�𐶐�
	/// </summary>
	public void InstantiationMyInstance();
	/// <summary>
	/// ���g�̃C���X�^���X���폜
	/// </summary>
	public void DeleteMyInstance();
	/// <summary>
	/// �V���O���g���Ȃ̂ɓ�ڂ��C���X�^���X���Ă�����G���[
	/// </summary>
	/// <param name="className">�N���X�̖��O</param>
	public void SingleTonError(string className);
	/// <summary>
	/// �X�N���v�g�����݂��Ȃ��������̃G���[
	/// </summary>
	/// <param name="className">���݂��Ȃ������N���X�̖��O</param>
	public void NullScriptError(string className);
	/// <summary>
	/// �I�u�W�F�N�g�����݂��Ȃ��������̃G���[
	/// </summary>
	/// <param name="objectName">���݂��Ȃ������I�u�W�F�N�g�̖��O</param>
	public void NullGameObjectError(string objectName);
	/// <summary>
	/// �V�[���������݂��Ȃ��������̃G���[
	/// </summary>
	/// <param name="sceneName">���݂��Ȃ������V�[����</param>
	public void NullSceneNameError(string sceneName);
	/// <summary>
	/// ���݂��Ȃ��͂��̃I�u�W�F�N�g�������������̃G���[
	/// </summary>
	/// <param name="objName">���݂��Ȃ��͂��̃I�u�W�F�N�g��</param>
	public void CantExistObject(string objName);
}
