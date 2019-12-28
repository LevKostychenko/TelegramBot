days = 0
pzm = 0

class Accumulation:
    def __init__(self, chis, dni):

        self.chis = chis
        self.dni = dni
        self.mas = [0]

    def calc1(self):
        i = 0
        self.mas[0] = self.chis
        if self.mas[0] < 1000:
            self.mas.append(self.mas[0] * 0.007 * 0.85 + self.mas[0])
        elif self.mas[0] < 10000:
            self.mas.append(self.mas[0] * 0.008 * 0.85 + self.mas[0])
        elif self.mas[0] >= 10000:
            self.mas.append(self.mas[0] * 0.009 * 0.85 + self.mas[0])
        n = 1
        while n <= self.dni:
            if self.mas[n] < 1000:
                self.mas.append(self.mas[n - 1] * 0.007 * 0.85 + self.mas[n])
            elif self.mas[n] < 10000:
                self.mas.append(self.mas[n - 1] * 0.008 * 0.85 + self.mas[n])
            elif self.mas[n] >= 10000:
                self.mas.append(self.mas[n - 1] * 0.009 * 0.85 + self.mas[n])
            n += 1

    def label1(self):
        k = 0
        s = ""
        while k <= self.dni:
            s += ("\n" + str(k) + " = " + str(self.mas[k]) + ";")
            k += 1
        return s

def calculateAccumulation(days, pzm):
    data3 = Accumulation(pzm, days)
    data3.calc1()
    return data3.label1()
