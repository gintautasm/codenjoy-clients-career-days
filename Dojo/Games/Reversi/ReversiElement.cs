/*-
 * #%L
 * Codenjoy - it's a dojo-like platform from developers to developers.
 * %%
 * Copyright (C) 2021 Codenjoy
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
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dojo.Games.Reversi
{
    public enum ReversiElement : short
    {

            // Пустое место – на которое во время хода можно ставить свою
            // фишку (если будет что перевернуть).

        NONE = (short)' ',

            // Препятствие на которое ставить фишку нельзя.

        BREAK = (short)'☼',

            // Так ты видишь белые (не свои) фишки, если ты играешь черными
            // и сейчас твой ход.

        WHITE = (short)'o',

            // Так ты видишь белые (свои) фишки, если ты играешь белыми и
            // сейчас не твой ход.

        WHITE_STOP = (short)'.',

            // Так всеми видятся белые фишки, если сейчас их ход.

        WHITE_TURN = (short)'O',

            // Так ты видишь черные (не свои) фишки, если ты играешь белыми
            // и сейчас твой ход.

        BLACK = (short)'x',

            // Так ты видишь черные (свои) фишки, если ты играешь черными и
            // сейчас не твой ход.

        BLACK_STOP = (short)'+',

            // Так всеми видятся черные фишки, если сейчас их ход.

        BLACK_TURN = (short)'X'
    }
}

