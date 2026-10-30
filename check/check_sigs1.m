clear all;
global inv_leng sig_leng mic_ch sp_ch;
set_parameter
dd_s = 0;
dd_n = 0;
for ss = 1:sp_ch
    [sig_sp1, ~] = audioread(sprintf('sig_sp_%d.wav', ss));
    [out_sp1, ~] = audioread(sprintf('out_%d.wav', ss));
    dd = sig_sp1 - out_sp1;
    dd_n = dd_n + sum(dd.^2);
    dd_s = dd_s + sum(sig_sp1.^2);
end
fprintf('Accuracy = %gdB\n', 10*log10(dd_s/dd_n));
