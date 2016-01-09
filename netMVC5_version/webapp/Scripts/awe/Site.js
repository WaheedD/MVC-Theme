//on document ready
function documentReady(root) {
    layPage();

    handleAnchors();
    
    $(document).on('aweload', 'table.awe-ajaxlist', wrapLists);

    $(window).on('resize', layPage);

    $('#btnLogo').click(function (e) {
        if ($(window).width() < 1050) {
            e.preventDefault();
            $('#btnMenuToggle').click();
        }
    });
    
    $('#btnMenuToggle').click(function () {
        MenuToggle($('#demoMenu').is(':visible'), 1);
    });

    $('pre').addClass('prettyprint');

    //show code 
    $('.code').hide().before('<br/>').before($('<span class="shcode">show code</span>').click(function () {
        var btn = $(this);
        btn.toggleClass("hideCode showCode");

        if (btn.hasClass("hideCode")) {
            btn.html("hide code");
            $(this).next().fadeIn();
        } else {
            btn.html("show code");
            $(this).next().fadeOut();
        }
    }));

    //tabs 
    $('.tabs').each(function (i, item) {
        var $tabstrip = $(item);
        var $tabs = $tabstrip.children();
        $tabs.wrapAll('<div class="tabscontent"/>');
        var $bar = $('<div class="tabsbar"></div>');
        $tabstrip.prepend($bar);
        $tabs.each(function (ti, tab) {
            var $tab = $(tab);
            var btnclass = "tab-btn";

            if (ti)
                $tab.hide();
            else
                btnclass += " active";

            var $btn = $('<button type="button" class="' + btnclass + '">' + $tab.data('caption') + '</button>');
            $btn.click(function () {
                $bar.children().removeClass('active');
                $btn.addClass('active');
                $tabs.hide();
                $tab.show();
            });
            $bar.append($btn);
        });
    });

    //change popup
    $('#chPopupMod').change(function () {
        var p = $('#chPopupMod').val();
        var theme = $('#chTheme').val();
        
        popupMod = p;

        $('.awe-popup').each(function () {
            if ($(this).data('api'))
                $(this).data('api').close();
        });

        $('.awe-multilookup, .awe-lookup').each(function () { $(this).data('api').initPopup(); });

        $.post(root + "Settings/Change", { theme: theme, popupMod: p });
    });

    $('#chTheme').change(function () {
        var newmod = $('#chPopupMod').val();
        var theme = $('#chTheme').val();
        
        $('#aweStyle').attr('href', root + "Content/themes/" + theme + "/AwesomeMvc.css?v=73");
        $('#modsStyle').attr('href', root + "Content/themes/" + theme + "/mods.css?v=73");
        $('body').attr('class', theme);
        $.post(root + "Settings/Change", { theme: theme, popupMod: newmod }, function () {
            setTimeout(function () {
                $('.awe-grid').each(
                    function () {
                        $(this).data('api').lay();
                    });
            }, 500);
        });
    });
}

function handleAnchors() {
    var anchor = location.hash.replace('#', '').replace(/\(|\)|:|\.|\;|\\|\/|\?|,/g, '');
    $('h3,h2').each(function (_, e) {
        var $e = $(e);
        var name = $e.data('name');
        if (!name) name = $.trim($e.html()).replace(/ /g, '-').replace(/\(|\)|:|\.|\;|\\|\/|\?|,/g, '');
        name = name.replace('&lt', '').replace('&gt', '');
        $e.append("<a class='anchor' name='" + name + "' href='#" + name + "' tabIndex='-1'>&nbsp;<i class='glyphicon glyphicon-link'></i></a>");
        if (name == anchor) {
            $e.addClass("awe-changing").removeClass('awe-changing', 3000);
        }
    });
}

var lastw = 0;

function layPage() {
    var ww = $(window).width();

    $("#main").css("min-height", ($(window).height() - 120) + "px");

    if (lastw != ww)
        MenuToggle(ww < 1050);
    lastw = ww;
}

var menuNodes;
function getMenuGridFunc(menuNodes) {
    function addParentsTo(res, node, all) {
        if (node.ParentId) {
            var isFound;
            $.each(res, function (_, o) {
                if (o.Id == node.ParentId) {
                    isFound = true;
                    return false;
                }
            });

            if (!isFound) {
                var parent = $.grep(all, function (o) { return o.Id == node.ParentId; })[0];
                res.push(parent);
                addParentsTo(res, parent, all);
            }
        }
    }
    
    function equals(a, b) {
        return new RegExp("^" + a + "$", "i").test(b);
    }

    function buildMenuGridModel(g) {
        var selectedItems = $.grep(menuNodes, function (o) {
            return equals(g.action, o.Action) &&
                equals(g.controller, o.Controller);
        });

        var selItems = selectedItems.slice(0);

        if (selectedItems.length) {
            selectedItems[0].Selected = "selected";
            $.each(selItems, function (_, item) {
                addParentsTo(selectedItems, item, menuNodes);
            });
        }

        $.each(selectedItems, function (_, o) {
            o.IsNodeSelected = true;
        });

        var words = g.search.split(" ");

        var regs = $.map(words, function (w) {
            return new RegExp(w, "i");
        });

        var regsl = regs.length;

        var result = $.grep(menuNodes, function (o) {
            var matches = 0;
            var s = o.Keywords + ' ' + o.Name;
            $.each(regs, function (_, reg) {
                reg.test(s) && matches++;
            });

            return regsl == matches;
        });

        var searchResult = result.slice(0);

        $.each(searchResult, function (_, o) {
            addParentsTo(result, o, menuNodes);
        });

        var rootNodes = $.grep(result, function (o) { return !o.ParentId; });

        var getChildren = function (node, nodeLevel) {
            return $.grep(result, function (o) { return o.ParentId == node.Id; });
        };

        function makeHeader(info) {
            var isNodeSelected = info.NodeItem.IsNodeSelected;
            return {
                Item: info.NodeItem,
                Collapsed: !g.search && info.NodeItem.Collapsed && !isNodeSelected,
                IgnorePersistence: g.search || isNodeSelected
            };
        }

        return utils.gridModelBuilder(this, g, rootNodes, { key: "Id", getChildren: getChildren, defaultKeySort: 1, forceSort: 1, makeHeader: makeHeader });
    }

    return function(gp) {
        var g = utils.getGridParams(gp);
        var url = this.tag.ItemsUrl;
        var storageKey = awe.ppk + "_menuNodes";
        if (menuNodes) {
            return buildMenuGridModel(g);
        }
        else if (localStorage && localStorage[storageKey]) {
            menuNodes = JSON.parse(localStorage[storageKey]);
            return buildMenuGridModel(g);
        } else {
            return $.post(url).then(function(items) {
                menuNodes = items;
                localStorage[storageKey] = JSON.stringify(items);
                return buildMenuGridModel(g);
            });
        }
    };
}

function renderMenuItem(o) {
    return o.Url ? "<a href='" + o.Url + "'>" + o.Name + "</a>" : o.Name;
}
//wrap ajaxlists for horizontal scrolling on small screens
function wrapLists() {
    $('table.awe-ajaxlist:not([wrapped])').each(function () {
        var columns = $(this).find('thead th').length;
        var mw = $(this).data('mw');
        if (columns || mw) {
            mw = mw || columns * 120;

            $(this).wrap('<div style="width:100%; overflow:auto;"></div>')
                .wrap('<div style="min-width:' + mw + 'px;padding-bottom:2px;"></div>')
                .attr('wrapped', 'wrapped');
        }
    });
}

function MenuToggle(hide) {
    if (hide) {
        $('#demoMenu').hide();
        $('#demoPage').css('margin-left', "0");
        $('#btnMenuToggle').show().removeClass('on');
        $('body').trigger('domlay');
    } else {
        $('#demoMenu').show();
        $('#demoPage').css('margin-left', "14.5em");
        $('#btnMenuToggle').addClass('on');
        $('body').trigger('domlay');
    }
}

/*begin*/
awe.err = function (o, xhr, textStatus, errorThrown) {
    var msg = "unexpected error occured";
    if (xhr) {
        msg = xhr.responseText;
    }
    var btnHide = $('<button type="button" class="awe-btn"> hide </button>').click(function () {
        $(this).parent().remove();
    });

    var c = $('<div/>').html(msg).append(btnHide);

    //decide where to attach the inline popup
    if (o.p && o.p.isOpen) { // if helper has popup and is open
        o.p.d.prepend(c); // put msg inside popup div
    } else if (o.f) {
        o.f.html(c); // put msg inside control field
    } else $('body').prepend(c);
};/*end*/

/*beginpopup*/
//this code uses popupMod variable specific to this demo, you shouldn't need this, 
//use utils.setPopup to be able to use the DropdownPopup and Inline (already called in utils.init)
function setAweDemoPopup() {
    var jqueryUIpopup = awe.popup;
    awe.popup = function (o) {
        if (o.tag && o.tag.DropdownPopup) {
            return awem.dropdownPopup(o);
        } else if (o.tag && o.tag.Inline) {
            return awem.inlinePopup(o);
        } else if (popupMod == 'inline') {
            return awem.inlinePopup(o);
        } else if (popupMod == 'bootstrap') {
            return awem.bootstrapPopup(o);
        } else {
            return jqueryUIpopup(o);
        }
    };
}
/*endpopup*/

var downloadLinks = [
        { K: "http://aspnetawesome.com/Download/MvcDemoApp", C: "Main Demo (this demo)" },
        { K: "http://aspnetawesome.com/Download/MvcMinSetupDemo", C: "Min Setup Demo (Template Project)" },
        { K: "http://aspnetawesome.com/Download/MvcTrial", C: "Trial version (dll, js, css for mvc 3/4/5)" },
        { K: "http://aspnetawesome.com/Download/SimpleDemo", C: "Simple Demo" },
        { K: "http://aspnetawesome.com/Download/VBnetDemo", C: "VB.net Demo" },
        { K: "http://aspnetawesome.com/Download/ProDinner", C: "ProDinner (uses EF, N-Tier, etc.)" },
        { K: "http://prodinner.aspnetawesome.com", C: "See ProDinner live" }
];

//google analytics
//var _gaq = _gaq || [];
//_gaq.push(['_setAccount', 'UA-27119754-1']);
//_gaq.push(['_setDomainName', 'aspnetawesome.com']);
//_gaq.push(['_trackPageview']);

//(function () {
//    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
//    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
//    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
//})();