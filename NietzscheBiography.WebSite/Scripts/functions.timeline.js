
$(document).ready(function () {
    displayTimeline();
    $('#timeline-filter-bar select').change(function () {
        validateDates();
        displayTimeline();
    });
});

function validateDates() {
    var startYear = $('#timeline-start-year option:selected').attr('value');
    var endYear = $('#timeline-end-year option:selected').attr('value');

    if ((startYear > 0) && (endYear > 0) && (startYear > endYear)) {
        $('#timeline-end-year').val(startYear);
    }
}

function displayTimeline() {

    $('#timeline').html('');

    var eventParticipantId = $('#timeline').attr('data-participant-id');
    var eventTypeId = $('#event-type option:selected').attr('value');
    var eventImportance = $('#event-importance option:selected').attr('value');
    var startYear = $('#timeline-start-year option:selected').attr('value');
    var endYear = $('#timeline-end-year option:selected').attr('value');

    var timelineData = [];

    var serviceUrl = '/timeline/data';

    $.ajaxSetup({
        async: false
    });

    $.getJSON(serviceUrl, {
        participantId: eventParticipantId,
        typeId: eventTypeId,
        importance: eventImportance,
        startYear: startYear,
        endYear: endYear
    })
	.done(function (data) {
	    $.each(data, function (i, event) {
	        if (event.start_date !== null) {

	            var eventBlockType = 'blog_post';
	            var eventBlockWidth = 400;

	            var timelineEvent = {
	                'type': eventBlockType,
	                'date': event.StartDate,
	                'title': event.Occurrence,
	                'width': eventBlockWidth,
	                'content': event.Title + '<br />' + getEventCitations(event.Citations)
	            }
	            timelineData.push(timelineEvent);
	        }
	    });
	});

    options = {
        animation: true,
        lightbox: false,
        showYear: false,
        allowDelete: false,
        columnMode: 'dual',
        order: 'asc'
    };

    var timeline = new Timeline($('#timeline'), timelineData);

    timeline.setOptions(options);
    timeline.display();

    $('.event-citation').popover({
        'placement': 'bottom',
        'trigger': 'hover'
    });
}

function getEventCitations(citations) {
    var eventCitations = [];
    var eventCitationsText = '';

    if (citations !== undefined) {
        $.each(citations, function (j, citation) {
            if ((citation.Text !== null) && citation.Text.length) {
                var citationLink = '<small class="event-citation" ';
                citationLink += 'data-toggle="popover" data-placement="top" ';
                citationLink += 'data-content="' + citation.Text + '" title="" ';
                citationLink += 'data-original-title="' + citation.Title + '">';
                citationLink += citation.SourceTitle + '</small>'
                eventCitations.push(citationLink);
            }
            else if ((citation.SourceTitle !== null) && citation.SourceTitle.length) {
                eventCitations.push('<small class="muted">' + citation.SourceTitle + '</small>');
            }
        });
    }

    if (eventCitations.length) {
        eventCitationsText = '<p class="text-center event-citation-source">';
        eventCitationsText += eventCitations.join('<br />');
        eventCitationsText += '</p>';
    }

    return eventCitationsText;
}
