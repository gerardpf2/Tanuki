using System.Collections.Generic;
using Infrastructure.System.Exceptions;
using Infrastructure.System.Parsing;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Parsing
{
    public class BoardParser : IBoardParser
    {
        [NotNull] private readonly IBoardSerializedDataConverter _boardSerializedDataConverter;
        [NotNull] private readonly IParser _parser;

        public BoardParser(
            [NotNull] IBoardSerializedDataConverter boardSerializedDataConverter,
            [NotNull] IParser parser)
        {
            ArgumentNullException.ThrowIfNull(boardSerializedDataConverter);
            ArgumentNullException.ThrowIfNull(parser);

            _boardSerializedDataConverter = boardSerializedDataConverter;
            _parser = parser;
        }

        public string Serialize(IBoard board)
        {
            BoardSerializedData boardSerializedData = _boardSerializedDataConverter.From(board);

            return _parser.Serialize(boardSerializedData);
        }

        public void Deserialize(string value, out IBoard board, out IEnumerable<PiecePlacement> piecePlacements)
        {
            BoardSerializedData boardSerializedData = _parser.Deserialize<BoardSerializedData>(value);

            _boardSerializedDataConverter.To(boardSerializedData, out board, out piecePlacements);
        }
    }
}