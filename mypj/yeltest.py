#!/usr/bin/python
#-*- coding:utf-8 -*-

def ter(a,b):
    print "old value a:%d b:%d"%(a,b)
    while True:
#        a,b,a=a+b,a-b,a-b
        a=a+b
        b=a-b
        a=a-b
        print "number a:%d  b:%d"%(a,b)
        yield 


if __name__=="__main__":
    t=ter(1,2)
    t.next()
    t.next()
    t.next()
    t.next()
    t.next()
    t.next()
    t.next()
    t.next()
    t.next()
    t.next()
    t.next()
    t.next()
    t.next()
