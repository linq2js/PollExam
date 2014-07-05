function loadPollSummary(url, chartContainer, onSuccess) {
    /// <summary>Load poll summary and bind data to chart</summary>
    // load poll summary
    $.ajax({
        url: url,
        success: function(data) {
            var chartData = [];
            // extract option summary
            $.each(data.OptionSummaries, function(index, optionSummary) {
                chartData.push({ data: optionSummary.Votes, label: optionSummary.Option.Description });
            });

            // extract other option summary
            chartData.push({ data: data.CustomOptionVotes, label: 'Other' });

            $.plot($(chartContainer), chartData, {
                series: {
                    pie: {
                        show: true
                    }
                },
                grid: {
                    hoverable: true,
                    clickable: true
                }
            });

            if (onSuccess) {
                onSuccess(chartData);
            }
        }
    });
}