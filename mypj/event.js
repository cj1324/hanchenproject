/**
 * @fileoverview javascript事件系统
 * @author wangxiang | [email]wxnet2013@gmail.com[/email]
 */
var elements = {};
var eventFns = {};
function addEvent(ele, evt, fn) {
	if (ele.addEventListener) {
		addEvent = function(ele, evt, fn) {
			ele.addEventListener(evt, fn, false);
		};
	} else {
		addEvent = function(ele, evt, fn) {
			var uid = "";
			var eleid = ele.evtid;
			var list = [];
			if (!eleid) {
				uid = 'element_uid_' + Math.floor(Math.random() * 2147483648).toString(36);
				ele.evtid = uid;
				elements[uid] = ele;
				eventFns[uid] = eventFns[uid] || {};
				eventFns[uid]["" + evt] = [];

				// 将之前注册过的事件，添加到事件系统中
				if (ele["on" + evt]) {
					eventFns[uid]["" + evt][0] = ele["on" + evt];
				}

				eventFns[uid]["" + evt].push(fn);
				ele["on" + evt] = function(event) {
					list = eventFns[uid]["" + evt];
					event = event || fixEvent(((this.ownerDocument || this.document || this).parentWindow || window).event);
					for (var i = 0, len = list.length; i < len; i++) {
						list[i].call(this, event);
					}
				};
			} else {
				//其它事件
				if (!eventFns[eleid]["" + evt]) {
					list = eventFns[eleid]["" + evt] = [];
					// 将之前注册过的事件，添加到事件系统中
					if (ele["on" + evt]) {
						eventFns[eleid]["" + evt][0] = ele["on" + evt];
					}
					ele["on" + evt] = function(event) {
						event = event || fixEvent(((this.ownerDocument || this.document || this).parentWindow || window).event);
						for (var i = 0, len = list.length; i < len; i++) {
							list[i].call(this, event);
						}
					};
				}
				eventFns[eleid]["" + evt].push(fn);
			}
		}
	}
	return addEvent(ele, evt, fn);
}

function removeEvent(ele, evt, fn) {
	if (ele.removeEventListener) {
		removeEvent = function(ele, evt, fn) {
			ele.removeEventListener(evt, fn, false);
		}
	} else {
		removeEvent = function(ele, evt, fn) {
			var eleid = ele.evtid;
			if (!eleid) return;
			var list = eventFns[eleid]["" + evt];
			for (var i = 0, len = list.length; i < len; i++) {
				if (fn === list[i]) {
					list.splice(i, 1);
				}
			}
		};
	}
	return removeEvent(ele, evt, fn);
}

// fixEvent written by Dean Edwards, 2005
//http://dean.edwards.name/my/events.js
function fixEvent(event) {
	// add W3C standard event methods
	event.preventDefault = fixEvent.preventDefault;
	event.stopPropagation = fixEvent.stopPropagation;

	//73到110行，取自jquery事件系统
	// Fix target property, if necessary
	if (!event.target) {
		event.target = event.srcElement || document; // Fixes #1925 where srcElement might not be defined either
	}

	// check if target is a textnode (safari)
	if (event.target.nodeType === 3) {
		event.target = event.target.parentNode;
	}

	// Add relatedTarget, if necessary
	if (!event.relatedTarget && event.fromElement) {
		event.relatedTarget = event.fromElement === event.target ? event.toElement: event.fromElement;
	}

	// Calculate pageX/Y if missing and clientX/Y available
	if (event.pageX == null && event.clientX != null) {
		var doc = document.documentElement,
		body = document.body;
		event.pageX = event.clientX + (doc && doc.scrollLeft || body && body.scrollLeft || 0) - (doc && doc.clientLeft || body && body.clientLeft || 0);
		event.pageY = event.clientY + (doc && doc.scrollTop || body && body.scrollTop || 0) - (doc && doc.clientTop || body && body.clientTop || 0);
	}

	// Add which for key events
	if (!event.which && ((event.charCode || event.charCode === 0) ? event.charCode: event.keyCode)) {
		event.which = event.charCode || event.keyCode;
	}

	// Add metaKey to non-Mac browsers (use ctrl for PC's and Meta for Macs)
	if (!event.metaKey && event.ctrlKey) {
		event.metaKey = event.ctrlKey;
	}

	/*
	// Add which for click: 1 === left; 2 === middle; 3 === right
	// Note: button is not normalized, so don't use it
	if (!event.which && event.button !== undefined) {
		event.which = (event.button & 1 ? 1: (event.button & 2 ? 3: (event.button & 4 ? 2: 0)));
	}
*/
	return event;
};
fixEvent.preventDefault = function() {
	this.returnValue = false;
};
fixEvent.stopPropagation = function() {
	this.cancelBubble = true;
};
