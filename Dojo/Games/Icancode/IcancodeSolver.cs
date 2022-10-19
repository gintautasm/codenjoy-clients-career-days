/*-
 * #%L
 * Codenjoy - it's a dojo-like platform from developers to developers.
 * %%
 * Copyright (C) 2012 - 2022 Codenjoy
 * %%
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as
 * published by the Free Software Foundation, either version 3 of the
 * License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public
 * License along with this program.  If not, see
 * <http://www.gnu.org/licenses/gpl-3.0.html>.
 * #L%
 */

namespace Dojo.Games.Icancode
{
    public class IcancodeSolver : ISolver
    {
        public string Get(IBoard gameBoard)
        {
            return Get(gameBoard as IcancodeBoard);
        }

        private string Get(IcancodeBoard gameBoard)
        {
            // solves level 4 or 5 
            int baseBoard = 0;
            int robotBoard = 1;

            var wallRight = gameBoard.GetRelativeElements(
                new List<IcancodeElement> { IcancodeElement.WALL_RIGHT, IcancodeElement.ANGLE_IN_RIGHT },
                baseBoard);
            var robo = gameBoard.GetRelativeElements(
                new List<IcancodeElement> { IcancodeElement.ROBO },
                robotBoard);

            if (wallRight.Any()
                && (gameBoard.IsNear(robo.FirstOrDefault(), IcancodeElement.WALL_RIGHT, baseBoard)
                || gameBoard.IsNear(robo.FirstOrDefault(), IcancodeElement.ANGLE_IN_RIGHT, baseBoard)))
            {
                return IcancodeCommand.MOVE_DOWN;
            }

            return IcancodeCommand.MOVE_RIGHT;
        }
    }
}
