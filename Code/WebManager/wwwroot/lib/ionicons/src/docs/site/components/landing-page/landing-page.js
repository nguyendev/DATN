var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { Component } from '@stencil/core';
let LandingPage = class LandingPage {
    render() {
        return h("div", null,
            h("h1", null,
                "The premium icon pack for ",
                h("a", { href: "http://ionicframework.com/" }, "Ionic Framework"),
                "."),
            h("h2", null, "100% free and open source. MIT Licensed."),
            h("div", null,
                h("span", { class: "twitter-share" },
                    h("a", { href: "https://twitter.com/share", class: "twitter-share-button", "data-via": "ionicframework", "data-hashtags": "icons,webdev,mobile", "data-related": "benjsperry,maxlynch,adamdbradley,drifty" }, "Tweet")),
                h("span", { class: "twitter-follow" },
                    h("a", { href: "https://twitter.com/ionicframework", class: "twitter-follow-button" }, "Follow @ionicframework")),
                h("span", { class: "github-star" },
                    h("iframe", { src: "http://ghbtns.com/github-btn.html?user=ionic-team&repo=ionicons&type=watch&count=true", allowtransparency: "true", frameborder: "0", scrolling: "0", width: "110", height: "20" }))),
            h("icon-search", null));
    }
};
LandingPage = __decorate([
    Component({
        tag: 'landing-page',
        styleUrl: 'landing-page.scss'
    })
], LandingPage);
export { LandingPage };
