window.nitro_lazySizesConfig = window.nitro_lazySizesConfig || {};
window.nitro_lazySizesConfig.lazyClass = "nitro-lazy";
nitro_lazySizesConfig.srcAttr = "nitro-lazy-src";
nitro_lazySizesConfig.srcsetAttr = "nitro-lazy-srcset";
nitro_lazySizesConfig.expand = 10;
nitro_lazySizesConfig.expFactor = 1;
nitro_lazySizesConfig.hFac = 1;
nitro_lazySizesConfig.loadMode = 1;
nitro_lazySizesConfig.ricTimeout = 50;
nitro_lazySizesConfig.loadHidden = true;
(function () {
    var e = null;
    var a = false;
    var t = false;
    var r = {
        childList: false,
        attributes: true,
        subtree: false,
        attributeFilter: ["src"],
        attributeOldValue: true
    };
    var i = null;
    var n = [];

    function o(e) {
        let t = n.indexOf(e);
        if (t > -1) {
            n.splice(t, 1);
            i.disconnect();
            c()
        }
        e.contentWindow.location.replace(e.getAttribute("nitro-og-src"))
    }

    function l() {
        if (!i) {
            i = new MutationObserver(function (e, a) {
                e.forEach(n => {
                    if (n.type == "attributes" && n.attributeName == "src") {
                        let t = n.target;
                        let r = t.getAttribute("nitro-og-src");
                        let i = t.src;
                        if (i != r) {
                            a.disconnect();
                            let e = i.replace(n.oldValue, "");
                            if (i.indexOf("data:") === 0 && ["?", "&"].indexOf(e.substr(0, 1)) > -1) {
                                if (r.indexOf("?") > -1) {
                                    t.setAttribute("nitro-og-src", r + "&" + e.substr(1))
                                } else {
                                    t.setAttribute("nitro-og-src", r + "?" + e.substr(1))
                                }
                            }
                            t.src = n.oldValue;
                            c()
                        }
                    }
                })
            })
        }
        return i
    }

    function s(e) {
        l().observe(e, r)
    }

    function c() {
        n.forEach(s)
    }

    function u() {
        window.removeEventListener("scroll", u);
        window.nitro_lazySizesConfig.expand = 300
    }
    window.addEventListener("scroll", u, {
        passive: true
    });
    window.addEventListener("NitroStylesLoaded", function () {
        a = true
    });
    window.addEventListener("load", function () {
        t = true
    });
    window.addEventListener("message", function (e) {
        if (e.data.action && e.data.action === "playBtnClicked") {
            var t = document.getElementsByTagName("iframe");
            for (var r = 0; r < t.length; r++) {
                if (e.source === t[r].contentWindow) {
                    o(t[r])
                }
            }
        }
    });
    document.addEventListener("lazybeforeunveil", function (t) {
        var r = false;
        var i = t.target.getAttribute("nitro-lazy-bg");
        if (i) {
            let e = t.target.style.backgroundImage.replace("data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAAAICTAEAOw==", i.replace(/\(/g, "%28").replace(/\)/g, "%29"));
            if (e === t.target.style.backgroundImage) {
                e = "url(" + i.replace(/\(/g, "%28").replace(/\)/g, "%29") + ")"
            }
            t.target.style.backgroundImage = e;
            r = true
        }
        var e = t.target.className.indexOf("elementor-invisible") != -1;
        if (e) {
            r = true;
            if (a) {
                t.target.className = t.target.className.replace("elementor-invisible", "")
            } else {
                window.addEventListener("NitroStylesLoaded", function (e) {
                    return function () {
                        e.className = e.className.replace("elementor-invisible", "")
                    }
                }(t.target))
            }
        }
        if (!r) {
            var n = t.target.tagName.toLowerCase();
            if (n !== "img" && n !== "iframe") {
                t.target.querySelectorAll("img[nitro-lazy-src],img[nitro-lazy-srcset]").forEach(function (e) {
                    e.classList.add("nitro-lazy")
                })
            }
        }
    });
    document.addEventListener("DOMContentLoaded", function () {
        document.querySelectorAll("iframe[nitro-og-src]").forEach(t => {
            if (t.getAttribute("nitro-og-src").indexOf("vimeo") > -1) {
                t.realGetAttribute = t.getAttribute;
                Object.defineProperty(t, "src", {
                    value: t.getAttribute("nitro-og-src"),
                    writable: false
                });
                Object.defineProperty(t, "getAttribute", {
                    value: function (e) {
                        if (e == "src") {
                            return t.realGetAttribute("nitro-og-src")
                        } else {
                            return t.realGetAttribute(e)
                        }
                    },
                    writable: false
                })
            }
            n.push(t)
        });
        c()
    })
})();
