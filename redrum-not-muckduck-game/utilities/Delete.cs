namespace redrum_not_muckduck_game
{
    class Delete
    {
        public static void Location(Room currentRoom)
        {
            int ROW_WHERE_LOCATION_STARTS = 1;
            int COLUMN_WHERE_LOCATION_STARTS = 16;

            for (int i = 0; i < currentRoom.Name.Length; i++)
            {
                Board.GameBoard[ROW_WHERE_LOCATION_STARTS, COLUMN_WHERE_LOCATION_STARTS + i] = ' ';
            }
        }

        public static void SceneTextArea()
        {
            int ROW_SCENE_ENDS = 20;
            int COL_SCENCE_STARTS = 1;
            int COL_SCENCE_ENDS = 48;

            for (int rowSceneStarts = 14; rowSceneStarts < ROW_SCENE_ENDS; rowSceneStarts++)
            {
                for (int col = 0; col < COL_SCENCE_ENDS; col++)
                {
                    Board.GameBoard[rowSceneStarts, COL_SCENCE_STARTS + col] = ' ';
                }
            }
        }
    }
}
