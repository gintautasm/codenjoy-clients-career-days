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

using Newtonsoft.Json;

namespace Dojo.Games.Icancode
{
    public class BoardWithLayers
    {
        public List<string> Layers { get; set; }
    }
    public class IcancodeBoard : AbstractLayeredBoard<IcancodeElement>
    {
        public List<string> Layers { get; set; }

        public IcancodeBoard(string boardString)
        {
            Layers = JsonConvert.DeserializeObject<BoardWithLayers>(boardString).Layers;
            // actuall map
            LengthXY = new LengthToXY(Size);
        }

        protected readonly LengthToXY LengthXY;

        /// <summary>
        /// GameBoard size (actual board size is Size x Size cells)
        /// </summary>
        public int Size
        {
            get
            {
                return (int)Math.Sqrt(Layers.FirstOrDefault().Length);
            }
        }

        public IcancodeElement GetAt(Point point, int layerId, IcancodeElement defaultValue = default)
        {
            if (point.IsOutOf(Size))
            {
                return defaultValue;
            }

            var value = Layers[layerId][LengthXY.GetLength(point.X, point.Y)];

            return (IcancodeElement)Enum.ToObject(typeof(IcancodeElement), value);
        }

        public bool IsAt(Point point, IcancodeElement element, int layerId)
        {
            if (point.IsOutOf(Size))
            {
                return false;
            }

            return EqualityComparer<IcancodeElement>.Default.Equals(GetAt(point, layerId: layerId), element);
        }

        public string BoardAsString()
        {
            var result = string.Empty;
            foreach (var layer in Layers)
            {
                for (int i = 0; i < Size; i++)
                {
                    result += layer.Substring(i * Size, Size);
                    result += "\n";
                }
                result += "\n\n";
            }

            return result;
        }

        public List<Point> GetRelativeElements(List<IcancodeElement> elements, int layerId)
        {
            var resultList = new List<Point>();

            foreach (var item in elements)
            {
                resultList.AddRange(Get(layerId, item).AsEnumerable());
            }

            resultList = resultList.Distinct().OrderBy(x => x.X).ThenBy(x => x.Y).ToList();
            return resultList;
        }

        public List<Point> Get(int layerId, params IcancodeElement[] elements)
        {
            var result = new List<Point>();

            for (int i = 0; i < Size * Size; i++)
            {
                var pt = LengthXY.GetXY(i);

                if (elements.Contains(GetAt(pt, layerId: layerId)))
                {
                    result.Add(pt);
                }
            }

            return result.OrderBy(pt => pt.X).ThenBy(pt => pt.Y).ToList();
        }

        public bool IsAnyOfAt(Point point, int layerId, params IcancodeElement[] elements)
        {
            return elements.Any(elem => IsAt(point, elem, layerId));
        }

        public bool IsNear(Point point, IcancodeElement element, int layerId)
        {
            if (point.IsOutOf(Size))
                return false;

            return IsAt(point.ShiftLeft(), element, layerId) ||
                   IsAt(point.ShiftRight(), element, layerId) ||
                   IsAt(point.ShiftTop(), element, layerId) ||
                   IsAt(point.ShiftBottom(), element, layerId);
        }

        public int CountNear(Point point, IcancodeElement element, int layerId)
        {
            if (point.IsOutOf(Size))
                return 0;

            int count = 0;
            if (IsAt(point.ShiftLeft(), element, layerId))
            {
                count++;
            }

            if (IsAt(point.ShiftRight(), element, layerId))
            {
                count++;
            }

            if (IsAt(point.ShiftTop(), element, layerId))
            {
                count++;
            }

            if (IsAt(point.ShiftBottom(), element, layerId))
            {
                count++;
            }

            return count;
        }



        public override string ToString()
        {
            return BoardAsString();
        }
    }
}
