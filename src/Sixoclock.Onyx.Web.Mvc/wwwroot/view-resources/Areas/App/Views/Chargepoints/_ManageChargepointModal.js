(function ($) {
    app.modals.ManageChargepointModal = function () {

        var _modalManager;
        var _$transactionsTable = $('#TransactionsTable');
        var _chargepointService = abp.services.app.chargepoint;
        var _transactionService = abp.services.app.transaction;
        
        var _$form = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form');
            _$form.validate();
        };

        $('#btnReset').click(function () {
            
            var resetEvent = new Object();
            resetEvent.chargepointId = $("#Id").val();
            resetEvent.resetTypeId = $("#ResetTypeId").val();
            resetEvent.resetType = $("#ResetTypeId option:selected").text();
            _chargepointService.createResetStatusAndEvent(resetEvent).done(function (data) {

                sweetAlert("Reset Command is sent");
            }).always(function () {
                _modalManager.setBusy(false);
            });
        });

        $("#EVSEId").change(function () {

            _chargepointService.getChargepointByIdForManageChargepoint({ id: $("#Id").val() }).done(function (data) {

                $("#evseStatus").text("Availability : " + data.chargepoint.evsEs[0].evseStatus);
                $("#evseAvailability").text("Status : " + data.chargepoint.evsEs[0].availabilityType);

                $("#EVSEStatus").val(data.chargepoint.evsEs[0].evseStatus);
                $("#AvailabilityType").val(data.chargepoint.evsEs[0].availabilityType);
            });
        });

        $("#AvailabilityTabEVSEId").change(function () {

            _chargepointService.getChargepointByIdForManageChargepoint({ id: $("#Id").val() }).done(function (data) {

                $("#AvailabilityTabEvseStatus").text("Availability : " + data.chargepoint.evsEs[0].evseStatus);
                $("#AvailabilityTabEvseAvailability").text("Status : " + data.chargepoint.evsEs[0].availabilityType);

                $("#EVSEStatus").val(data.chargepoint.evsEs[0].evseStatus);
                $("#AvailabilityType").val(data.chargepoint.evsEs[0].availabilityType);
            });
        });

        $("#UnlockTabEVSEId").change(function () {

            _chargepointService.getChargepointByIdForManageChargepoint({ id: $("#Id").val() }).done(function (data) {

                $("#UnlockTabEvseStatus").text("Availability : " + data.chargepoint.evsEs[0].evseStatus);
                $("#UnlockTabEvseAvailability").text("Status : " + data.chargepoint.evsEs[0].availabilityType);

                $("#EVSEStatus").val(data.chargepoint.evsEs[0].evseStatus);
                $("#AvailabilityType").val(data.chargepoint.evsEs[0].availabilityType);
            });
        });
        
        var dataTable = _$transactionsTable.DataTable({

            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _transactionService.getTransactionsByChargepoint
            },
            columnDefs: [
                {
                    targets: 0,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    rowAction: {
                        targets: 0,
                        data: null,
                        orderable: false,
                        defaultContent: '',
                        element: $("<button/>")
                            .addClass("btn btn-default btn-xs")
                            .attr("title", app.localize('Stop Charging'))
                            .append($("<i/>").addClass("fa fa-times")).click(function () {
                                var record = $(this).data();
                                stopCharging(record);
                            })
                    }
                },
                {
                    targets: 1,
                    data: "transactionStartTime"
                },
                {
                    targets: 2,
                    data: "evsE_id"
                },
                {
                    targets: 3,
                    data: "transactionStatusValue"
                },
                {
                    targets: 4,
                    data: "transactionType"
                },
                {
                    targets: 5,
                    data: "remoteReason"
                },
                {
                    targets: 6,
                    data: "meterType"
                },
                {
                    targets: 7,
                    data: "transactionStartUserName"
                }
            ]
        });
        
        function stopCharging(data) {
           
            var remoteStopTransaction = new Object();
            remoteStopTransaction.chargepointId = $("#Id").val();
            remoteStopTransaction.evsEId = data.evseId;
            remoteStopTransaction.transactionId = data.id;

            _chargepointService.createRemoteStopTransaction(remoteStopTransaction).done(function (data) {

                sweetAlert("Stop Charging Command is sent");
            });
        }
        function getTransactionsByChargepoints() {

            dataTable.ajax.reload({ id: $("#Id").val() });
        }
    };
    
})(jQuery);

function searchUsers() {
    
    var _tagService = abp.services.app.tag;
    
    _tagService.getUserByIdToken($("#txtIdToken").val()).done(function (result) {

        var htm = '<p> Name = ' + result.name + ' \n</p>';

        $("#UserInfo").html(htm);
        $("#TagId").val(result.id);
    });
}
function startCharging() {

    var _chargepointService = abp.services.app.chargepoint;
    var _remoteStartStopEvent = abp.services.app.remoteStartStopEvent;

    if ($("#AvailabilityType").val() != "Operative") {
        sweetAlert("EVSE not operative");
        return;
    }
    else if ($("#EVSEStatus").val() != "Available") {
        sweetAlert("EVSE not Available");
        return;
    }
    else if ($("#txtIdToken").val() == "" || $("#TagId").val() == "") {
        sweetAlert("Please search user tag.");
        return;
    }
    
    var remoteStartTransaction = new Object();
    remoteStartTransaction.chargepointId = $("#Id").val();
    remoteStartTransaction.eVSEId = $("#EVSEId").find(":selected").val();
    remoteStartTransaction.tagId = $("#TagId").val();
    remoteStartTransaction.tagIdToken = $("#txtIdToken").val();
    remoteStartTransaction.remoteStartStopStatusId = 0;
    remoteStartTransaction.remoteStartStopEventTypeId = 0;
    
    _chargepointService.createRemoteStartTransaction(remoteStartTransaction).done(function (data) {

        sweetAlert("Start Charging Command is sent");

        _remoteStartStopEvent.getRemoteStartStopEventsListByChargepoint({ id: $("#Id").val() }).done(function (data) {

            var html = "";
            for (var i = 0; i < data.items.length; i++) {
                html += "<tr><td>" + data.items[i].messageEventResponse + "</td ><td>" + data.items[i].evsE_id + "</td><td>" + data.items[i].eventType + "</td><td>" + data.items[i].statusValue + "</td><td>" + data.items[i].created + "</td></tr>"
            }
            $("#remoteStartStopEventsTable").html(html);
        });
    });
}
function updateEvseAvailability() {

    var _chargepointService = abp.services.app.chargepoint;
    
    var avail = $("#AvailabilityTypeId").find(":selected").text();
    var evse = $("#AvailabilityTabEVSEId").find(":selected").text();
    if (avail == "" || evse == "0") {
        sweetAlert("please select availability type and Evse.");
        return;
    }
    var availability = new Object();
    availability.chargepointId = $("#Id").val();
    availability.availabilityId = avail;
    availability.availabilityTypeId = $("#AvailabilityTypeId").val();
    availability.AvailabilityTabEVSEId
    availability.eVSEId = evse;

    _chargepointService.updateAvailability(availability).done(function (data) {

        sweetAlert("Update Evse availability command is sent to CP.");

    });
}
function unlockEVSE() {
    
    var _chargepointService = abp.services.app.chargepoint;
    var _unlockEventService = abp.services.app.unlockEvent;
    
    var evse = $("#UnlockTabEVSEId").find(":selected").text();
    if (evse == "0") {
        sweetAlert("please select Evse.");
        return;
    }
    var availability = new Object();
    availability.chargepointId = $("#Id").val();
    availability.eVSEId = $("#UnlockTabEVSEId").find(":selected").val();
    availability.eVSE_Id = evse;
    _chargepointService.unlockEVSE(availability).done(function (data) {

        sweetAlert("Unlock Evse command is sent to CP.");

        _unlockEventService.getUnlockEventsListByChargepoint({ id: $("#Id").val() }).done(function (data) {

            var html = "";
            for (var i = 0; i < data.items.length; i++) {
                
                html += "<tr><td>" + data.items[i].date + "</td ><td>" + data.items[i].messageEventResponse + "</td><td>" + data.items[i].evsE_Id + "</td><td>" + data.items[i].unlockStatusValue + "</td></tr>"
            }
            $("#UnlockEventsTable").html(html);
        });

    });
}
function clearCache() {

    var _chargepointService = abp.services.app.chargepoint;
    var _clearCacheEventService = abp.services.app.clearCacheEvent;
    
    var cacheEvent = new Object();
    cacheEvent.chargepointId = $("#Id").val();
    cacheEvent.clearCacheEventId = 0;
    
    _chargepointService.clearCacheChargepoint(cacheEvent).done(function (data) {

        sweetAlert("Clear cache command is sent to CP.");

        _clearCacheEventService.getClearCacheEventsListByChargepoint({ id: $("#Id").val() }).done(function (data) {

            var html = "";
            for (var i = 0; i < data.items.length; i++) {

                html += "<tr><td>" + data.items[i].date + "</td ><td>" + data.items[i].messageEventResponse + "</td><td>" + data.items[i].clearCacheStatusValue + "</td></tr>"
            }
            $("#UnlockEventsTable").html(html);
        });

    });
}