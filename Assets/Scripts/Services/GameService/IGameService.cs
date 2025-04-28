namespace Arenar.Services
{
	public interface IGameService
	{
		void StartGame(GameData gameData);

		void EndGame();
	}
}