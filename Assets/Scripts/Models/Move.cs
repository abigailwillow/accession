namespace Accession.Models {
    public class Move {
        /// <summary>
        /// The cell to move to.
        /// </summary>
        public Cell cell { get; private set; }
        /// <summary>
        /// The piece to move.
        /// </summary>
        public Piece instigator { get; private set; }
        /// <summary>
        /// The piece to jump over, if any.
        /// </summary>
        public Piece target { get; private set; }
        /// <summary>
        /// Whether or not this move is a jump.
        /// </summary>
        public bool isJump => target != null;

        public Move(Cell cell, Piece instigator, Piece target = null) {
            this.cell = cell;
            this.instigator = instigator;
            this.target = target;
        }

        /// <summary>
        /// Executes this move.
        /// </summary>
        public void Execute() => instigator.Move(cell);
    }
}