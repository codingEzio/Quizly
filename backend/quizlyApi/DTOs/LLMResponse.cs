namespace quizlyApi.DTOs;


public class LLMRawResponse
{
    // #TODO cuz there were many LLMs out there
}

public class LLMQuizOptionResponse
{
    public LLMQuizResponseMetadata metadata { get; set; }
    public List<LLMQuizResponseContent> content { get; set; }
}

public class LLMQuizResponseContent
{
    public string problem { get; set; }
    public string answer { get; set; }
    public string answer_dissection { get; set; }
    public List<string> options { get; set; }
}

public class LLMQuizResponseMetadata
{
    public int total_q { get; set; }
}


public class LLMUtilResponse
{
    public bool _success { get; set; }
    public string? _message { get; set; }

    public string? RawContent { get; set; }
    public string? PostProcessedContent { get; set; }
}
