var current_text = '';
var previous_text = '';

var name_sort_asc = true;
var data = [];

var OnLoad = function () {

    GetData();

    var input = document.getElementById('terms');
    input.addEventListener("keydown", function (e) {
        current_text = this.value;
        if (e.keyCode == 13) {
            e.preventDefault();
        }
    });
}

var ClearTerms = function () {
    var input = document.getElementById('terms');
    input.value = '';
    Render(Sort(data, name_sort_asc));
}

function getRule(rule) {
    return new RegExp("^" + rule.split("*").join(".*") + "$", 'i');
}

var InputTimer = function () {
    if (current_text != previous_text) {
        if (current_text != '') {

            var result = [];

            var t = current_text.split(' ');
            var rules = [];

            $.each(t, function (index, val) {
                rules.push(getRule('*' + val + '*'));
            });

            $.each(data, function (index, val) {

                var index = 0;

                $.each(rules, function (j, rule) {

                    if (rule.test(val.name))++index;
                });

                if (index >= rules.length) result.push(val);
            });
            Render(Sort(result, name_sort_asc));
        }
        else Render(Sort(data, name_sort_asc));
    }

    previous_text = current_text;
}

var GetItemHtml = function (val, index) {
    var items = [];

    var name = "<label class='name'>" + val.name + "</label>";

    items.push(name);
    items.push("<label class='phone'>");

    if (val.numbers.length > 0) {

        var index = 1;
        $.each(val.numbers, function (key, n) {

            var number = "<label class='number'>" + index + ")&nbsp;" + n.value + "</label>";
            var status = "<label class='status' onclick='UpdateStatus(" + val.customerID + "," + n.id + ");'>" + (n.active == true ? "Active" : "Inactive") + "</label>";

            items.push(number);
            items.push(status);

            ++index;
        });
    }

    items.push("</label>");
   
    return items;
}

var Render = function (data) {

    var items = [];

    $.each(data, function (index, val) {
        items.push("<div class='item'>");
        items.push(...GetItemHtml(val, index + 1));
        items.push("</div>");
    });

    $("#results").html(items.join(""));
}

var GetData = function () {

    document.getElementById('loading').style.display = 'inline';

    $.get('api/customer').done(function (source) {
        data = [];
        
        $.each(source, function (key, val) {
                data.push(val);
        });

        Render(Sort(data, name_sort_asc));

        previous_text = '';

        document.getElementById('loading').style.display = 'none';
    });
}

var UpdateStatus = function (customer_id, number_id) {

    $.ajax({
        url: "/api/customer/" + customer_id + "/" + number_id,
        type: "PUT",
        accepts: "application/json",
        contentType: "application/json",
        success: function () {

            GetData();
        },
        error: function () {
        }
    });
}

var FlipSort = function () {

    if (name_sort_asc == true) name_sort_asc = false;
    else name_sort_asc = true;
    
    var input = document.getElementById('terms');
    if (input.value != '') {

        previous_text = '';
        InputTimer();

    } else {

        Render(Sort(data, name_sort_asc));

    }        
}

var Sort = function (source, asc) {

    var output = source.slice(0);    
    var swaps = 0;

    do {
        swaps = 0;

        for (var i = 0; i < output.length - 1; ++i) {

            var swap = false;

            if ((asc == true) && (output[i].name > output[i + 1].name)) swap = true;
            else if ((asc == false) && (output[i].name < output[i + 1].name)) swap = true;

            if (swap) {

                var temp = output[i];
                output[i] = output[i + 1];
                output[i + 1] = temp;

                ++swaps;
            }
        }

    } while (swaps > 0);

    return output;
}