namespace L4dOpenMatchMakingPlatform.Backend.DTO
{
    public class QueryLobbySummaryListResponseDTO
    {
        public IEnumerable<LobbySummary> lobby_summary_list { get; set; }

        public int page_index { get; set; }

        public int page_size { get; set; }

        public int page_count { get; set; }

        public int total_count { get; set; }
    }
}
