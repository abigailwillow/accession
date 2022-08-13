using Accession.Controllers;

namespace Accession.Models {
    public class Move {
        /// <summary>
        /// The cell to move to.
        /// </summary>
        public CellController cell { get; private set; }
        /// <summary>
        /// The piece to move.
        /// </summary>
        public PieceController instigator { get; private set; }
        /// <summary>
        /// The piece to jump over, if any.
        /// </summary>
        public PieceController target { get; private set; }
        /// <summary>
        /// Whether or not this move is a jump.
        /// </summary>
        public bool isJump => target != null;

        public Move(CellController cell, PieceController instigator, PieceController target = null) {
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