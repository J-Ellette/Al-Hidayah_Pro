namespace AlHidayahPro.Data.Models;

/// <summary>
/// Represents a question in a quiz
/// </summary>
public class QuizQuestion
{
    public int Id { get; set; }
    
    /// <summary>
    /// Quiz ID
    /// </summary>
    public int QuizId { get; set; }
    
    /// <summary>
    /// Question text
    /// </summary>
    public string QuestionText { get; set; } = string.Empty;
    
    /// <summary>
    /// Question type (multiple_choice, true_false, fill_blank, matching)
    /// </summary>
    public string QuestionType { get; set; } = "multiple_choice";
    
    /// <summary>
    /// Order in the quiz
    /// </summary>
    public int OrderIndex { get; set; }
    
    /// <summary>
    /// Points for this question
    /// </summary>
    public int Points { get; set; } = 1;
    
    /// <summary>
    /// Explanation/reference for the answer
    /// </summary>
    public string? Explanation { get; set; }
    
    /// <summary>
    /// Options (JSON array for multiple choice)
    /// </summary>
    public string? Options { get; set; }
    
    /// <summary>
    /// Correct answer(s) (JSON)
    /// </summary>
    public string CorrectAnswer { get; set; } = string.Empty;
    
    // Navigation property
    public Quiz? Quiz { get; set; }
}
