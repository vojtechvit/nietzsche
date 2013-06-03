String.prototype.format = function () {
    a = this;
    for (k in arguments) {
        a = a.replace("{" + k + "}", arguments[k]);
    }
    return a;
};

$('body').on('hidden', '.modal', function () {
    $(this).removeData('modal');
});

$(function () {

    var eventParticipantId = $('#map_canvas').attr('data-participant-id');
    var eventTypeId = $('#event-type option:selected').attr('value');

    var mapCenter = '47,7';
    var mapZoom = 4;

    if (parseInt(eventTypeId) > 0) {
        mapCenter = '47,-45';
        mapZoom = 3;
    }

    $('#map_canvas').gmap({
        'center': mapCenter,
        'zoom': mapZoom,
        'disableDefaultUI': true
    }).bind('init', function (evt, map) {

        $('#map_canvas').gmap('addControl', 'radios', google.maps.ControlPosition.TOP_LEFT);

        var serviceFetchLocationsUrl = '/places/data';
        var serviceFetchLocationEventsUrl = '/timeline/data';
        serviceFetchLocationEventsUrl += '?participantId=' + eventParticipantId + '&format=html';
        if (eventTypeId) serviceFetchLocationEventsUrl += '&typeId=' + eventTypeId;

        $.ajaxSetup({
            async: false
        });

        $.getJSON(serviceFetchLocationsUrl, {
            participantId: eventParticipantId,
            eventTypeId: eventTypeId
        })
		.done(function (data) {
		    $.each(data.locations, function (i, location) {
		        var thisEventTypes = [];

		        $.each(location.events, function (j, event) {
		            if (jQuery.inArray(event.event_type, thisEventTypes) == -1) {
		                thisEventTypes.push(event.event_type);
		            }
		        });

		        $('#map_canvas').gmap(
					'addMarker', {
					    'tags': thisEventTypes,
					    'bound': true,
					    'position': new google.maps.LatLng(parseFloat(location.latitude), parseFloat(location.longitude))
					}).click(function () {
					    var locationDescription = '<a href="/place-' + location.location_id + '">';
					    locationDescription += location.title + '</a><br />';
					    locationDescription += '<a class="btn btn-small" data-toggle="modal" ';
					    locationDescription += 'href="' + serviceFetchLocationEventsUrl + '&locationId=' + location.location_id + '" ';
					    locationDescription += 'data-target="#location-events-modal"><i class="icon-list-alt"></i> See Events</a>';
					    $('#map_canvas').gmap('openInfoWindow', { 'content': locationDescription }, this);
					});
		    });

		    var insertedEventTypeFilters = 0;
		    $.each(data.event_type_occurrences, function (eventType, i) {
		        if (insertedEventTypeFilters >= 10) { return; }
		        $('#radios fieldset').append(('<label class="checkbox"><input type="checkbox" value="{0}" />{1}</label>').format(eventType, eventType + ' (' + i + ')'));
		        insertedEventTypeFilters++;
		    });
		});

        $('input:checkbox').click(function () {

            $('#map_canvas').gmap('closeInfoWindow');
            $('#map_canvas').gmap('set', 'bounds', null);

            var filters = [];

            $('input:checkbox:checked').each(function (i, checkbox) {
                filters.push($(checkbox).val());
            });

            if (filters.length > 0) {
                $('#map_canvas').gmap('find', 'markers',
					{ 'property': 'tags', 'value': filters, 'operator': 'OR' },
					function (marker, found) {
					    if (found) {
					        $('#map_canvas').gmap('addBounds', marker.position);
					    }
					    marker.setVisible(found);
					});
            }
            else {
                $.each($('#map_canvas').gmap('get', 'markers'), function (i, marker) {
                    $('#map_canvas').gmap('addBounds', marker.position);
                    marker.setVisible(true);
                });
            }
        });

    });
});
