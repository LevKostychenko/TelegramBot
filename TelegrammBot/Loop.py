days = 0
pzm = 0
addedPzm = 0

class Loop:
    def __init__(self, chis, dop, dni):

        self.chis = chis

        self.dni = dni
        self.dop = dop
        self.mas = [0]


    def calc2(self):
        global t

        t = 63
        i = 0
        self.mas[0] = self.chis
        if self.mas[0] < 1000:
            self.mas.append(self.mas[0] * 0.007 * 0.85 + self.mas[0])
        elif self.mas[0] < 10000:
            self.mas.append(self.mas[0] * 0.008 * 0.85 + self.mas[0])
        elif self.mas[0] >= 10000:
            self.mas.append(self.mas[0] * 0.009 * 0.85 + self.mas[0])
        n = 1
        while n <= self.dni-1:
            if n % 30 == 0:
                if self.mas[n] < 1000:
                    self.mas.append(self.mas[n - 1] * 0.007 * 0.85 + self.mas[n] + self.dop)
                elif self.mas[n] < 10000:
                    self.mas.append(self.mas[n - 1] * 0.008 * 0.85 + self.mas[n] + self.dop)
                elif self.mas[n] >= 10000:
                    self.mas.append(self.mas[n - 1] * 0.009 * 0.85 + self.mas[n] + self.dop)
            else:
                if self.mas[n] < 1000:
                    self.mas.append(self.mas[n - 1] * 0.007 * 0.85 + self.mas[n])
                elif self.mas[n] < 10000:
                    self.mas.append(self.mas[n - 1] * 0.008 * 0.85 + self.mas[n])
                elif self.mas[n] >= 10000:
                    self.mas.append(self.mas[n - 1] * 0.009 * 0.85 + self.mas[n])
            n += 1


    def label2(self):
        k = 0

        s = ""
        while k <= self.dni:
            s += ("\n" + str(k) + " = " + str(self.mas[k]) + ";")
            k += 1
        return s

def calculateLoop(days, pzm, addedPzm):
    data2 = Loop(pzm, addedPzm, days)
    data2.calc2()
    return data2.label2()


print(calculateLoop(100, 20, 20))