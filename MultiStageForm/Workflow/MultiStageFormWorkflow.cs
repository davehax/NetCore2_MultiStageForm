using System;
using System.Threading.Tasks;
using MultiStageForm.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore;


namespace MultiStageForm.Workflow
{
    public enum MultiStageFormStages {
        First = 1,
        Second = 2,
        Third = 3,
        Finished = 4
    }

    public class MultiStageFormWorkflow
    {
        private MultiStageFormContext _context;

        public MultiStageFormWorkflow(MultiStageFormContext context)
        {
            _context = context;
        }

        public async Task<bool> OnMultiStageFormCreate(Stagedform stagedform)
        {
            // Create a new Stage1, Stage2, and Stage3 form then update the StagedForm
            Stage1 newStage1 = new Stage1();
            newStage1.Name = "";
            _context.Stage1.Add(newStage1);

            Stage2 newStage2 = new Stage2();
            newStage2.Description = "";
            _context.Stage2.Add(newStage2);

            Stage3 newStage3 = new Stage3();
            newStage3.Date = DateTime.Now;
            _context.Stage3.Add(newStage3);

            await _context.SaveChangesAsync();

            stagedform.Stage1 = newStage1.Id;
            stagedform.Stage2 = newStage2.Id;
            stagedform.Stage3 = newStage3.Id;
            stagedform.CurrentStage = (int)MultiStageFormStages.First;
            _context.Update(stagedform);

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Moves the form to the indicated stage.
        /// </summary>
        /// <returns>The form to stage.</returns>
        /// <param name="stage1">Stage1.</param>
        /// <param name="stage">Stage.</param>
        public async Task<bool> MoveFormToStage(Stage1 stage1, MultiStageFormStages stage)
        {
            Stagedform stagedform = await _context.Stagedform.SingleOrDefaultAsync(s => s.Stage1 == stage1.Id);
            return await MoveFormToStage(stagedform, stage);
        }

        /// <summary>
        /// Moves the form to the indicated stage.
        /// </summary>
        /// <returns>The form to stage.</returns>
        /// <param name="stage2">Stage2.</param>
        /// <param name="stage">Stage.</param>
        public async Task<bool> MoveFormToStage(Stage2 stage2, MultiStageFormStages stage)
        {
            Stagedform stagedform = await _context.Stagedform.SingleOrDefaultAsync(s => s.Stage2 == stage2.Id);
            return await MoveFormToStage(stagedform, stage);
        }

        /// <summary>
        /// Moves the form to the indicated stage.
        /// </summary>
        /// <returns>The form to stage.</returns>
        /// <param name="stage3">Stage3.</param>
        /// <param name="stage">Stage.</param>
        public async Task<bool> MoveFormToStage(Stage3 stage3, MultiStageFormStages stage)
        {
            Stagedform stagedform = await _context.Stagedform.SingleOrDefaultAsync(s => s.Stage3 == stage3.Id);
            return await MoveFormToStage(stagedform, stage);
        }

        /// <summary>
        /// Moves the supplied form to the indicated stage.
        /// </summary>
        /// <returns><c>true</c>, if form to stage was moved, <c>false</c> otherwise.</returns>
        /// <param name="stagedform">Stagedform.</param>
        /// <param name="stage">Stage.</param>
        private async Task<bool> MoveFormToStage(Stagedform stagedform, MultiStageFormStages stage)
        {
            if (stagedform != null) 
            {
                stagedform.CurrentStage = (int)stage;
                _context.Update(stagedform);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
