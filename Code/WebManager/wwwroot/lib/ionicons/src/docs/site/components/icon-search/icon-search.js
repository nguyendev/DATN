var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { Component, Listen } from '@stencil/core';
let LandingPage = class LandingPage {
    keyup(ev) {
        console.log('keyup', ev);
    }
    focusout(ev) {
        console.log('focusout', ev);
    }
    focusin(ev) {
        console.log('focusin', ev);
    }
    render() {
        return h("div", { class: "icon-search" },
            h("div", { class: "search" },
                h("input", { id: "search", type: "search", placeholder: "Search" })));
    }
};
__decorate([
    Listen('keyup')
], LandingPage.prototype, "keyup", null);
__decorate([
    Listen('focusout')
], LandingPage.prototype, "focusout", null);
__decorate([
    Listen('focusin')
], LandingPage.prototype, "focusin", null);
LandingPage = __decorate([
    Component({
        tag: 'icon-search',
        styleUrl: 'icon-search.scss'
    })
], LandingPage);
export { LandingPage };
